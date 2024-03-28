using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="Alt consume",menuName ="Ability/HealthConsume")]
public class HealthEat : AbilitySO
{
    [SerializeField] float consumeRange;
    [SerializeField] LayerMask enemyLayer;
    [SerializeField] float healthGainBase;
    [SerializeField] float enemyPercentage;
    public override void ActivateAbility(GameObject owner)
    {
        base.ActivateAbility(owner);
        Debug.Log("bite");
        Collider[] colliders = Physics.OverlapSphere(owner.transform.position, consumeRange, enemyLayer);
        foreach (Collider col in colliders)
        {
            Debug.Log("Bite" + colliders.Length);
            EnemyStats enemy = col.GetComponent<EnemyStats>();
            if (enemy != null && enemy.isDead)
            {
                HealthManager healthManager = owner.GetComponent<HealthManager>();
                PlayerStats playerStats = owner.GetComponent<PlayerStats>();

                float enemyHealth;
                enemyHealth = enemy.maxHealth * enemyPercentage;



                float Temphealth;
                Temphealth = playerStats.currentHealth;
                Temphealth += healthGainBase + enemyHealth;

                playerStats.currentHealth = Temphealth;

                if(Temphealth > playerStats.maxHealth)
                {
                    Debug.Log("Overhealth gained");
                    playerStats.maxHealth += 5;
                }
            }
            Destroy(col.gameObject);
        }
    }
}
