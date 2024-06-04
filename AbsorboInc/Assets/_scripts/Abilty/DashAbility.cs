using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using static UnityEngine.RuleTile.TilingRuleOutput;

[CreateAssetMenu(fileName = "Dash", menuName = "Ability/Dash")]
public class DashAbility : AbilitySO
{
    public float dashDistance = 5f;


    public float speedMultiplier;
    public float speedDuration;
    private float defaultSpeed;
    private NavMeshAgent agent;
    public override void ActivateAbility(GameObject owner)
    {
        base.ActivateAbility(owner);

        agent = owner.GetComponent<NavMeshAgent>();

        Vector3 mousePosition = Input.mousePosition;
        mousePosition.z = Camera.main.transform.position.y;

        Vector3 targetPosition = Camera.main.ScreenToWorldPoint(mousePosition);
        targetPosition.y = owner.transform.position.y;

        Vector3 dashDirection = (targetPosition - owner.transform.position).normalized;

        Vector3 dashDestination = owner.transform.position + dashDirection * dashDistance;

        defaultSpeed = owner.GetComponent<PlayerStats>().currentMovementSpeed;

        owner.GetComponent<PlayerStats>().currentMovementSpeed *= speedMultiplier;
        owner.GetComponent<MonoBehaviour>().StartCoroutine(ResetSpeed(owner));
        if (NavMesh.SamplePosition(dashDestination, out NavMeshHit hit, 1.0f, NavMesh.AllAreas))
        {
            agent.SetDestination(hit.position);
        }
    }
    private IEnumerator ResetSpeed(GameObject owner)
    {
        yield return new WaitForSeconds(speedDuration);
        owner.GetComponent<PlayerStats>().currentMovementSpeed = defaultSpeed;
    }
}
