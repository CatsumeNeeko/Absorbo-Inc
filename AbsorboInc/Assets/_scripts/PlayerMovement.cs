using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerMovement : MonoBehaviour
{
    [Header("dependencies")]
    public PlayerStats playerStats;
    public GameManager manager;
    [Header("Movement Info")]
    [SerializeField] Camera cam;
    [SerializeField] float speed;
    private NavMeshAgent navMeshAgent;
    [Header("AutoAttack info")]
    [SerializeField] GameObject autoAttackGO;
    [Header("Consume Info")]
    public float consumeRange = 5f;
    public LayerMask enemyLayer;
    [Header("Ability Indicator Info")]
    [SerializeField] GameObject abilityIndicator;
    private float abilityIndicatorBase = 100;
    private Vector3 originalScale;
    private bool rc1 = false, rc2 = false, rc3 = false;
    private void Awake()
    {
        cam = Camera.main;
        playerStats = GetComponent<PlayerStats>();
    }
    private void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        originalScale = abilityIndicator.transform.localScale;
    }
    private void Update()
    {
        navMeshAgent.speed = playerStats.currentMovementSpeed;
        if (Input.GetMouseButton(1) || Input.GetMouseButtonDown(1))
        {
            //Debug.Log("Rightclick pressed");
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hit))
            {
                string objectTag = hit.collider.tag;
                //Debug.Log(hit.collider.gameObject);
                switch (objectTag)
                {
                    case "Ground":
                        navMeshAgent.SetDestination(hit.point);
                        //Debug.Log(hit.point);
                        break;
                    case "Enemy":
                        AutoAttack(hit);
                        break;
                    case "PickUp":
                        navMeshAgent.SetDestination(hit.point);
                        break;
                }
            }
        }
        #region Ability Casting 

        //Consume
        if (Input.GetKeyUp(KeyCode.E))
        {
            abilityIndicator.SetActive(false);
            abilityIndicator.transform.localScale = originalScale;
            Consume();
            playerStats.ConsumeTimer();
        }
        if (Input.GetKeyDown(KeyCode.E))
        {
            abilityIndicator.SetActive(true);
            abilityIndicator.transform.localScale = new Vector3(abilityIndicatorBase * consumeRange,abilityIndicatorBase * consumeRange);
        }



        if (Input.GetKeyUp(KeyCode.Q))
        {
            //playerStats.AbilityOneTimer();
            abilityIndicator.SetActive(false);
            abilityIndicator.transform.localScale = originalScale;
            playerStats.ActivateFirstAbility(gameObject);
            playerStats.AbilityOneTimer();
            rc1 = false;
        }
        if (Input.GetKeyDown(KeyCode.Q))
        {
            float scaleIncrease = playerStats.abilities[0].abilityRange;
            if (playerStats.abilities[0].hasRange)
            {
                abilityIndicator.SetActive(true);
                abilityIndicator.transform.localScale = new Vector3(abilityIndicatorBase * scaleIncrease, abilityIndicatorBase * scaleIncrease);
                OnDrawGizmos();
                rc1 = true;
            }
        }


        if (Input.GetKeyUp(KeyCode.W))
        {
            abilityIndicator.SetActive(false);
            abilityIndicator.transform.localScale = originalScale;
            playerStats.ActivateSecondAbility(gameObject);
            playerStats.AbilityTwoTimer();
            rc2 = false;
        }
        if (Input.GetKeyDown(KeyCode.W))
        {
            float scaleIncrease = playerStats.abilities[1].abilityRange;
            if (playerStats.abilities[1].hasRange)
            {
                abilityIndicator.SetActive(true);
                abilityIndicator.transform.localScale = new Vector3(abilityIndicatorBase * scaleIncrease, abilityIndicatorBase * scaleIncrease);
                OnDrawGizmos();
                rc2 = true;
            }
        }


        if (Input.GetKeyUp(KeyCode.R))
        {
            abilityIndicator.SetActive(false);
            abilityIndicator.transform.localScale = originalScale;
            playerStats.ActivateThirdAbility(gameObject);
            playerStats.AbilityThreeTimer();
            rc3 = false;
        }
        if (Input.GetKeyDown(KeyCode.R))
        {
            float scaleIncrease = playerStats.abilities[2].abilityRange;
            if (playerStats.abilities[2].hasRange)
            {
                abilityIndicator.SetActive(true);
                abilityIndicator.transform.localScale = new Vector3(abilityIndicatorBase * scaleIncrease, abilityIndicatorBase * scaleIncrease);
                OnDrawGizmos();
                rc3 = true;
            }
        }

        #endregion
    }

    public void AutoAttack(RaycastHit hit)
    {
        
        if (playerStats.canAuto)
        {
            playerStats.AutoTimer();
            GameObject autoBullet = Instantiate(autoAttackGO, transform.position, Quaternion.identity);
            PlayerAutoBullet playerAutoBullet = autoBullet.GetComponent<PlayerAutoBullet>();
            if(playerAutoBullet != null)
            {
                playerAutoBullet.SetTarget(hit.transform);
            }
        }


    }
    /// <summary>
    /// Idea one: Uses a box collider around the player and will check if any of the enemies around are dead if they are it will get the id of the enemy and add that ID to the first empty array slot in player stats. 
    /// if it isnt empty, remove the ID from slot five and shuffle the rest to the right and place the ID into the first slot
    /// Idea two: use a layermask on the enemies then check if any enemies are in range then check if they are an enemy and are dead
    /// 
    /// 
    /// </summary>
    public void Consume()
    {
        Debug.Log("Consume Active");
        Collider[] colliders = Physics.OverlapSphere(transform.position, consumeRange, enemyLayer);
        OnDrawGizmos();
        foreach (Collider col in colliders)
        {
            EnemyStats enemy = col.GetComponent<EnemyStats>();
            if (enemy != null && enemy.isDead)//Checks if enemy is dead
            {
                bool stomachFull = true;
                for (int i = 0; i < playerStats.stomachArray.Length; i++)//checks if any values in array are zero if yes set stomach full to false
                {
                    if(playerStats.stomachArray[i] == 0)
                    {
                        stomachFull = false;
                        break;
                    }
                }
                if (stomachFull)//If stomach is full remove oldest value in array and set slot zero as the value of the enemy eaten
                {
                    
                    for (int i = playerStats.stomachArray.Length - 1; i > 0; i--)
                    {
                        playerStats.stomachArray[i] = playerStats.stomachArray[i - 1];
                        
                    }
                    playerStats.stomachArray[0] = enemy.enemyID;
                }
                else//will just add the value to first avaliable empty slot
                {
                    for (int i = 0; i < playerStats.stomachArray.Length; i++)
                    {
                        if (playerStats.stomachArray[i] == 0)
                        {
                            playerStats.stomachArray[i] = enemy.enemyID;
                            break;
                        }
                    }
                }
                manager.IncreaseTimer(enemy.stabilityValue);


                ////Random chance to increase stability
                //float stabilityChanceValue = Random.Range(0.0f, 1.0f);
                //if (stabilityChanceValue < enemy.stabilityChance)
                //{
                //    Debug.Log(stabilityChanceValue + " < " + enemy.stabilityChance + " success");
                //    GameManager gameManager = GetComponent<GameManager>();
                //    gameManager.stabilityTimer += enemy.stabilityValue;
                //}
                //else
                //    Debug.Log(stabilityChanceValue + " < " + enemy.stabilityChance + " fail");


                Destroy(col.gameObject);
            }

        }
    }
    private void OnDrawGizmos()
    {
        //Gizmos.color = Color.cyan;
        //Gizmos.DrawWireSphere(transform.position, consumeRange);
        if (rc1)
        {
            Gizmos.color = Color.magenta;
            Gizmos.DrawWireSphere(transform.position, playerStats.abilities[0].abilityRange);
        }
    }
}
