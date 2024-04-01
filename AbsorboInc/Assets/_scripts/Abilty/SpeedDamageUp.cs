using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName ="ChoiceBoost",menuName ="Ability/ChoiceBoost")]
public class SpeedDamageUp : AbilitySO
{
    public float baseSpeed;
    public float bonusSpeed;
    public float baseDamage;
    public float bonusDamage;
    public bool switchDamageSpeed = true;
    public bool switchNoticed = true;
    public override void ActivateAbility(GameObject owner)
    {
        base.ActivateAbility(owner);
        if(switchDamageSpeed )
        {
            switchDamageSpeed = false;
            switchNoticed = true;
            SwapStats(owner);
        }
        else
        {
            switchDamageSpeed = true;
            switchNoticed = true;
            SwapStats(owner);
        }

    }

    private void SwapStats(GameObject owner)
    {
        
        if (switchDamageSpeed)
        {
            switchNoticed = false;

            baseSpeed = owner.GetComponent<PlayerStats>().currentMovementSpeed;
            owner.GetComponent<PlayerStats>().currentMovementSpeed *= bonusSpeed;

            owner.GetComponent<PlayerStats>().currentAutoDamage = baseDamage;
        }
        else
        {
            switchNoticed = false;
            baseDamage = owner.GetComponent<PlayerStats>().currentAutoDamage;
            owner.GetComponent<PlayerStats>().currentAutoDamage *= bonusDamage;

            owner.GetComponent<PlayerStats>().currentMovementSpeed = baseSpeed;
        }
    }
}
