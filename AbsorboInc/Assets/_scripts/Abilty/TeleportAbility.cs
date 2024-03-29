using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
[CreateAssetMenu(fileName = "New Teleport Ability", menuName = "Ability/Teleport Ability")]
public class TeleportAbility : AbilitySO
{
    public float teleportRange = 10f;
    
    public override void ActivateAbility(GameObject owner)
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, teleportRange))
        {
            // Check if the hit object is on a NavMesh surface
            NavMeshHit navHit;
            if (NavMesh.SamplePosition(hit.point, out navHit, 0.1f, NavMesh.AllAreas))
            {
                // Teleport the player using NavMeshAgent.Warp
                NavMeshAgent navAgent = owner.GetComponent<NavMeshAgent>();
                if (navAgent != null)
                {
                    navAgent.Warp(navHit.position);
                    Debug.Log("Teleporting player to " + navHit.position);
                }
                else
                {
                    Debug.LogWarning("no NavMeshAgent Found");
                }
            }
            else
            {
                Debug.LogWarning("surface doesnt have a navmesh");
            }
        }
        else
        {
            Debug.LogWarning("hit isnt in range");
        }
    }
}
