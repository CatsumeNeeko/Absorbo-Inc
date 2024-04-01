using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealSummon : MonoBehaviour
{
    public float detectionRange;
    public float healthDrain;
    public float drainFail;
    public float healAmmount;

    public float despawnTime;
    public float tickInterval;
    public float despawnTimer;
    public float drainTimer;

    private void Update()
    {
        drainTimer += Time.deltaTime;

        if (drainTimer >= tickInterval)
        {
            CheckForEnemies();
            drainTimer = 0f;
        }

        despawnTimer += Time.deltaTime;
        if (despawnTimer >= despawnTime)
        {
            Destroy(gameObject);
        }   
    }
    private void CheckForEnemies()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, detectionRange);
        List<EnemyStats> enemies = new List<EnemyStats>();
        foreach (Collider collider in colliders)
        {
            EnemyStats enemy = collider.GetComponent<EnemyStats>();
            if (enemy != null)
            {
                enemies.Add(enemy);
            }
            
        }
        DrainEnemies(enemies);
    }
    private void DrainEnemies(List<EnemyStats> enemies )
    {
        if(enemies.Count == 0)
        {
            GameObject.FindGameObjectWithTag("Player").GetComponent<HealthManager>().TakeDamage(drainFail);
            return;
        }
        foreach (EnemyStats enemy in enemies)
        {
            
            enemy.GetComponent<HealthManager>().TakeDamage(healthDrain);

            healAmmount += healthDrain;

            GameObject.FindGameObjectWithTag("Player").GetComponent<HealthManager>().HealDamage(healAmmount);
        }
    }
}