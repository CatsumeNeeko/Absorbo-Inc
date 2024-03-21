using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
[CreateAssetMenu(fileName ="SpeedUp",menuName ="Ability/SpeedUp")]
public class SpeedUp : AbilitySO
{
    public float speedMultiplier;
    public float speedDuration;
    private float defaultSpeed;


    public override void ActivateAbility(GameObject owner)
    {
        base.ActivateAbility(owner);
        defaultSpeed = owner.GetComponent<PlayerStats>().currentMovementSpeed;

        owner.GetComponent<PlayerStats>().currentMovementSpeed *= speedMultiplier;
        owner.GetComponent<MonoBehaviour>().StartCoroutine(ResetSpeed(owner));
    }
    private IEnumerator ResetSpeed(GameObject owner)
    {
        yield return new WaitForSeconds(speedDuration);
        owner.GetComponent<PlayerStats>().currentMovementSpeed = defaultSpeed;
    }
}
