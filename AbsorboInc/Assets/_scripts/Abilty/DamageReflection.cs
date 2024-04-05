using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName ="Reflect",menuName ="Ability/Reflect")]
public class DamageReflection : AbilitySO
{
    public float baseDamage;
    public float reflectPercent;
    //public float detectionRange;
    public override void DamageTook(GameObject owner,float damage)
    {
        base.DamageTook(owner,damage);

        Collider[] colliders = Physics.OverlapSphere(owner.transform.position, abilityRange);
        List<EnemyStats> enemies = new List<EnemyStats>();
        foreach (Collider collider in colliders)
        {
            EnemyStats enemy = collider.GetComponent<EnemyStats>();
            if (enemy != null)
            {
                enemies.Add(enemy);
            }
        }
        DealDamage(enemies , damage);
    }
    private void DealDamage(List<EnemyStats> enemies , float damage)
    {
        foreach (EnemyStats enemy in enemies)
        {
            float reflectDamage = damage * reflectPercent;

            float totalDamage = baseDamage + reflectDamage;

            enemy.GetComponent<HealthManager>().TakeDamage(totalDamage);
        }

    }
}
