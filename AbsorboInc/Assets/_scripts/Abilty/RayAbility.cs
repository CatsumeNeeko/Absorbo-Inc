using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="Ray",menuName ="Ability/Ray")]
public class RayAbility : AbilitySO
{
    public float abilityRange;
    public float abilityTotalDamage;
    public float abilityDamageReduction;

    public override void ActivateAbility(GameObject owner)
    {
        base.ActivateAbility(owner);

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit[] hits = Physics.RaycastAll(owner.transform.position, ray.direction, abilityRange);
        Debug.DrawRay(owner.transform.position, ray.direction * abilityRange, Color.red);
        int hitCount = 0;
        foreach (RaycastHit hit in hits)
        {
            //EnemyStats enemyStats = hit.collider.GetComponent<EnemyStats>();
            HealthManager healthManager = hit.collider.GetComponent<HealthManager>();
            if (healthManager != null)
            {
                float damage = abilityTotalDamage - (abilityDamageReduction * hitCount);

                
                healthManager.TakeDamage(damage);
                hitCount++;

            }
        }
    }
}
