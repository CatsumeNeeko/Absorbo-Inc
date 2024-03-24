using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerSummon : MonoBehaviour
{
    public float detectionRange;
    public float totalDamageOutput;

    public float despawnTime;
    public float tickInterval;
    public float despawnTimer;
    public float damageTimer;

    private void Update()
    {
        damageTimer += Time.deltaTime;
        if (damageTimer >= tickInterval)
        {
            CheckForEnemies();
            damageTimer = 0f;
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
        DealDamage(enemies);
    }
    private void DealDamage(List<EnemyStats> enemies)
    {
        if (enemies.Count == 0)
            return;

        foreach(EnemyStats enemy in enemies)
        {
            float totalDamage = totalDamageOutput / enemies.Count;
            enemy.GetComponent<HealthManager>().TakeDamage(totalDamage);
        }
    }
}
