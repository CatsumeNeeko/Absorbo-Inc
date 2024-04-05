using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using static UnityEngine.GraphicsBuffer;

public class CommanderSummon : MonoBehaviour
{
    [Header("Despawn")]
    public float despawnTime;
    public float despawnTimer;


    [Header("Detection")]
    public float detectionRange;
    public float attackRange;

    [Header("Cooldown/Damage")]
    public float attackCooldown;
    [SerializeField] float currentCooldown;
    [SerializeField] bool canAttack;
    [SerializeField] float attackDamage;


    public Transform selectedTarget;
    public LayerMask enemyLayer;
    private NavMeshAgent navMeshAgent;

    private void Awake()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        InvokeRepeating("EnemySearch", 0f, 0.5f);
    }
    void Update()
    {
        despawnTimer += Time.deltaTime;
        if (despawnTimer >= despawnTime)
        {
            Destroy(gameObject);
        }
        if (selectedTarget != null && canAttack)
        {
            if (Vector3.Distance(transform.position, selectedTarget.position) <= attackRange)
            {
                selectedTarget.gameObject.GetComponent<HealthManager>().TakeDamage(attackDamage);

                StartCoroutine(AttackCooldown());
            }
            else
            {
                navMeshAgent.SetDestination(selectedTarget.position);
            }
        }
    }

    void EnemySearch()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, detectionRange, enemyLayer);

        if (colliders.Length > 0)
        {
            Transform closestEnemy = null;
            float closestDistance = Mathf.Infinity;

            foreach (Collider collider in colliders)
            {
                float distance = Vector3.Distance(transform.position, collider.transform.position);
                if (distance < closestDistance)
                {
                    closestEnemy = collider.transform;
                    closestDistance = distance;
                }
            }

            // Set the closest enemy as the target
            if (closestEnemy != null)
            {
                selectedTarget = closestEnemy;
            }

        }
    }
    public void SetTarget(Transform newTarget)
    {
        selectedTarget = newTarget;
    }


    private IEnumerator AttackCooldown()
    {
        canAttack = false;
        yield return new WaitForSeconds(attackCooldown);
        canAttack = true;
    }
}
