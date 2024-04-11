using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName ="EatingProjectile",menuName ="Ability/EatProjectile")]
public class EatingShot : AbilitySO
{
    public bool enemyEaten = false;
    public GameObject enemyCorpse;
    [SerializeField] LayerMask enemyLayer;

    public override void ActivateAbility(GameObject owner)
    {
        base.ActivateAbility(owner);

        if(enemyEaten)
        {
            ShootEnemy(owner);
        }
        else
        {
            CheckToEat(owner);
        }
    }

    void CheckToEat(GameObject owner)
    {
        Collider[] colliders = Physics.OverlapSphere(owner.transform.position, abilityRange, enemyLayer);

        foreach (Collider col in colliders)
        {
            Debug.Log("Bite" + colliders.Length);
            EnemyStats enemy = col.GetComponent<EnemyStats>();
            if (enemy != null && enemy.isDead)
            {
                enemyEaten = true; 
                Destroy(col);
                break;
            }
        }
    }
    void ShootEnemy(GameObject owner)
    {

    }
}
