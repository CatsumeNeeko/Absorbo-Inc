using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName ="Dodge",menuName ="Ability/Dodge")]
public class DodgeAbility : AbilitySO
{
    public float baseDodgeChance;
    public float dodgeChance;
    public float dodgeIncrease;
    public override void DamageTook(GameObject owner,  float damage)
    {
        base.DamageTook(owner,damage);
        Debug.Log("Damage taken");
        float dodgeRoll = Random.value;

        if (dodgeRoll <= dodgeChance)
        {
            Debug.Log("Player dodged the attack!");
            HealthManager healthManager = owner.GetComponent<HealthManager>();
            healthManager.HealDamage(damage);
            //Debug.Log(damage);
            dodgeChance = baseDodgeChance;
        }
        else
        {
            Debug.Log("Fail dodge");
            dodgeChance = dodgeChance + dodgeIncrease;
            Debug.Log(dodgeChance);
        }
    }
}
