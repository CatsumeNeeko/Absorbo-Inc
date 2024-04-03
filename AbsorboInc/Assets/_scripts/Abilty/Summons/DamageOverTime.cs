using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class DamageOverTime : MonoBehaviour
{
    [SerializeField] float detectionRange;
    [SerializeField] float damagePerTik;
    [SerializeField] float damageEvents = 0;

    [SerializeField] float despawnTime;
    [SerializeField] float tickInterval;
    [SerializeField] float despawnTimer;
    [SerializeField] float poisionTimer;
    [SerializeField] float timeBetweenPoision;

    public void Update()
    {
        poisionTimer += Time.deltaTime;

        if(poisionTimer >= tickInterval)
        {
            CheckForEnemies();
            poisionTimer = 0;
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
        PoisonEnemies(enemies);
    }
    private void PoisonEnemies(List<EnemyStats> enemies)
    {
        StartCoroutine("Poisioned",enemies);
    }

    IEnumerator Poisioned(List<EnemyStats> enemies)
    {
        if(damageEvents < 5)
        {
            yield return new WaitForSeconds(timeBetweenPoision);
            foreach(EnemyStats enemy in enemies)
            {
                enemy.GetComponent<HealthManager>().TakeDamage(damagePerTik);
                damageEvents++;
                StartCoroutine("Poisioned");
            }
        }
    }
}
