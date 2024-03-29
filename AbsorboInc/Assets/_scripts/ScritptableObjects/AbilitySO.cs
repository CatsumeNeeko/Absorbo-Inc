using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Ability", menuName = "Ability")]
public class AbilitySO : ScriptableObject
{
    public new string name;
    public bool isPassive;
    public float cooldown;

    public virtual void ActivateAbility(GameObject owner)
    {

    }
    public virtual void DamageTook(GameObject owner,float damage)
    {
        //Debug.Log("Base TakeDamage method called. Damage: " + damage);
    }
    public virtual void ActivatePassive(GameObject owner)
    {

    }
}
