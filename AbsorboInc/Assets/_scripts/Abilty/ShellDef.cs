using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName ="DefUp",menuName ="Ability/DefUp")]
public class ShellDef : AbilitySO
{
    public float buffDuration;
    public float defMultiplyer;
    public float speedDecrease;
    private float originalDef;
    private float originalSpeed;


    public override void ActivateAbility(GameObject owner)
    {
        base.ActivateAbility(owner);
        originalDef = owner.GetComponent<PlayerStats>().currentDefense;
        originalSpeed = owner.GetComponent<PlayerStats>().currentMovementSpeed;

        owner.GetComponent<PlayerStats>().currentMovementSpeed *= speedDecrease;
        owner.GetComponent<PlayerStats>().currentDefense *= defMultiplyer;

        owner.GetComponent<MonoBehaviour>().StartCoroutine(ResetStats(owner));
    }

    private IEnumerator ResetStats(GameObject owner)
    {
        yield return new WaitForSeconds(buffDuration);
        owner.GetComponent<PlayerStats>().currentDefense = originalDef;
        owner.GetComponent<PlayerStats>().currentMovementSpeed = originalSpeed;
    }
}
