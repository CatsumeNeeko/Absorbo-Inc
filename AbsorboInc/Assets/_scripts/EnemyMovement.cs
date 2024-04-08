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
    [SerializeField] GameObject shootLocation;
    [SerializeField] float speed;
    private float lastAttackTime;
    public float distanceToTarget;
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
        navMeshAgent.speed = enemies.currentMovementSpeed;
        if (enemies.isDead == false)
        {
            distanceToTarget = Vector3.Distance(transform.position, target.position);

            if (enemies.isRanged)
            {
                if (distanceToTarget > enemies.attackRange)
                {
                    navMeshAgent.SetDestination(target.position);
                }
                else
                {
                    navMeshAgent.ResetPath();
                }
            }
            else
            {
                if (distanceToTarget > enemies.attackRange)
                {
                    navMeshAgent.SetDestination(target.position);
                }
            }
            if (Time.time - lastAttackTime >= enemies.attackCD)
            {
                lastAttackTime = Time.time;
                if (enemies.isRanged)
                {
                    float bulletRandomness = Random.Range(0f, 1f);
                    RangedAttack(bulletRandomness);
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
    void RangedAttack(float random)
    {
        //create projectile 
        transform.LookAt(target);

        Vector3 targetPosition = target.position + Random.insideUnitSphere * random;
        Vector3 direction = (targetPosition - transform.position).normalized;
        
        GameObject bullet = Instantiate(enemies.projectile, shootLocation.transform.position, Quaternion.identity);
        Rigidbody rigidbody = bullet.GetComponent<Rigidbody>();
        rigidbody.velocity = direction * enemies.projectileSpeed;

        EnemyBullets enemyBullets = bullet.GetComponent<EnemyBullets>();
        enemyBullets.damage = enemies.damage;
    }
}
