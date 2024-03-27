using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="AoeDamage",menuName ="Ability/AoeDamage")]
public class AoeDamage : AbilitySO
{
    public float detectionRange;
    public float percentageDamage;
    public float minDamage;

    public override void ActivateAbility(GameObject owner)
    {
        base.ActivateAbility(owner);

        Collider[] colliders = Physics.OverlapSphere(owner.transform.position, detectionRange);
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
        foreach(EnemyStats enemy in enemies)
        {
            float baseDamage = minDamage;

            float percentDamage = percentageDamage * (1f - enemy.currentHealth / enemy.maxHealth);

            float totalDamage = baseDamage + percentDamage;

            enemy.GetComponent<HealthManager>().TakeDamage(totalDamage);
        }

    }
}
