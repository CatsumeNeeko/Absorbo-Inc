using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class EnemyMovement : MonoBehaviour
{
    [Header("Dependencies")]
    public EnemyStats enemies;


    [Header("NovementInfo")]
    [SerializeField] Transform target;
    private NavMeshAgent navMeshAgent;

    [Header("ScriptInfo")]
    [SerializeField] bool isRanged;
    [SerializeField] float speed;
    private float lastAttackTime, distanceToTarget;
    private void Awake()
    {
        enemies = GetComponent<EnemyStats>();
        navMeshAgent = GetComponent<NavMeshAgent>();
        speed = enemies.currentMovementSpeed;
        navMeshAgent.speed = speed;
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if(player != null)
        {
            target = player.transform;
        }

    }
    private void Update()
    {
        if (enemies.isDead == false)
        {
            navMeshAgent.SetDestination(target.position);
            if (Time.time - lastAttackTime >= enemies.attackCD)
            {
                lastAttackTime = Time.time;
                if (isRanged)
                {

                }
                else
                {
                    MeleeAttack();
                }
            }
        }
        else if (enemies.isDead)
        {
            GetComponent<NavMeshAgent>().enabled = false;
        }
    }

    void MeleeAttack()
    {
        distanceToTarget = Vector3.Distance(transform.position, target.position);
        if(distanceToTarget < enemies.attackRange)
        {
            HealthManager healthManager = target.GetComponent<HealthManager>();
            if(healthManager != null)
            {
                healthManager.TakeDamage(enemies.damage);
            }
        }
    }
    void RangedAttack()
    {
        //create projectile 
    }
}
