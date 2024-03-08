using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerMovement : MonoBehaviour
{
    [Header("dependencies")]
    public PlayerStats playerStats;
    [Header("Movement Info")]
    [SerializeField] Camera cam;
    [SerializeField] float speed;
    private NavMeshAgent navMeshAgent;
    [Header("AutoAttack info")]
    [SerializeField] GameObject autoAttackGO;
    [Header("Consume Info")]
    public float consumeRange = 5f;
    public LayerMask enemyLayer;


    private void Awake()
    {
        cam = Camera.main;
        playerStats = GetComponent<PlayerStats>();
    }
    private void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
    }
    private void Update()
    {
        if (Input.GetMouseButton(1) || Input.GetMouseButtonDown(1))
        {
            //Debug.Log("Rightclick pressed");
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hit))
            {
                string objectTag = hit.collider.tag;

                switch (objectTag)
                {
                    case "Ground":
                        navMeshAgent.SetDestination(hit.point);
                        //Debug.Log("GroundHit");
                        break;
                    case "Enemy":
                        AutoAttack(hit);

                        //need to fix the autoattack so it shoots out and doesnt repeat the event 100 of times a second 

                        break;
                    case "PickUp":
                        navMeshAgent.SetDestination(hit.point);
                        break;
                }
            }
        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            Consume();
            playerStats.ConsumeTimer();
        }
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



            //autoBullet.GetComponent<PlayerAutoBullet>().target = hit.transform;
            //Vector3 direction = (hit.point - transform.position).normalized;
            //Rigidbody bulletRigidbody = autoBullet.GetComponent<Rigidbody>();
            //bulletRigidbody.velocity = direction * 10f;

            //Physics.IgnoreCollision(autoBullet.GetComponent<Collider>(), GetComponent<Collider>());
        }

        //HealthManager health = hit.collider.GetComponent<HealthManager>();
        //if(health != null)
        //{
        //    health.TakeDamage(1);
        //}



        //Transform target = hit.collider.transform;
        //Vector3 spawnPosition = transform.position;
        //GameObject spawnedObject = Instantiate(autoAttackGO, spawnPosition, Quaternion.identity);
        //Vector3 direction = (hit.point - spawnPosition).normalized;
        //spawnedObject.GetComponent<Rigidbody>().velocity = direction * speed;
    }
    /// <summary>
    /// Idea one: Uses a box collider around the player and will check if any of the enemies around are dead if they are it will get the id of the enemy and add that ID to the first empty array slot in player stats. 
    /// if it isnt empty, remove the ID from slot five and shuffle the rest to the right and place the ID into the first slot
    /// Idea two: use a layermask on the enemies then check if any enemies are in range then check if they are an enemy and are dead
    /// </summary>
    public void Consume()
    {
        Debug.Log("Consume Active");
        Collider[] colliders = Physics.OverlapSphere(transform.position, consumeRange, enemyLayer);
        foreach (Collider col in colliders)
        {
            EnemyStats enemy = col.GetComponent<EnemyStats>();
            if (enemy != null && enemy.isDead)
            {
                //Debug.Log("Found a dead enemy!");
                //Debug.Log(enemy.enemyID);
                //for (int i = 0; i < playerStats.stomachArray.Length; i++)
                //{
                //    if(playerStats.stomachArray[i] == 0)
                //    {
                //        playerStats.stomachArray[i] = enemy.enemyID;                       
                //        break;
                //    }

                //}
                bool stomachFull = true;
                for (int i = 0; i < playerStats.stomachArray.Length; i++)
                {
                    if(playerStats.stomachArray[i] == 0)
                    {
                        stomachFull = false;
                        break;
                    }
                }
                if (stomachFull)
                {
                    // Remove oldest slot by shifting elements to the right
                    for (int i = playerStats.stomachArray.Length - 1; i > 0; i--)
                    {
                        playerStats.stomachArray[i] = playerStats.stomachArray[i - 1];
                        
                    }

                    // Add the new value to the first slot
                    playerStats.stomachArray[0] = enemy.enemyID;
                }
                else
                {
                    // Add the value to the first available slot
                    for (int i = 0; i < playerStats.stomachArray.Length; i++)
                    {
                        if (playerStats.stomachArray[i] == 0)
                        {
                            playerStats.stomachArray[i] = enemy.enemyID;
                            break;
                        }
                    }
                }
            }

        }
    }
    //private void OnDrawGizmosSelected()
    //{
    //    // Visualize detection radius in editor
    //    Gizmos.color = Color.red;
    //    Gizmos.DrawWireSphere(transform.position, consumeRange);
    //}
}
