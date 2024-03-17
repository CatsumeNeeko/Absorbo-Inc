using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
[CreateAssetMenu(fileName = "New Teleport Ability", menuName = "Ability/Teleport Ability")]
public class TeleportAbility : AbilitySO
{
    public float teleportRange = 10f;
    Camera cam = Camera.main;
    public override void ActivateAbility(GameObject owner)
    {
        base.ActivateAbility(owner);
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, teleportRange))
        {
            // Check if the hit object has a NavMeshAgent component
            NavMeshAgent navAgent = hit.collider.GetComponent<NavMeshAgent>();
            if (navAgent != null)
            {
                // Teleport the player to the hit position
                owner.transform.position = hit.point;
                Debug.Log("Teleporting player to " + hit.point);
            }
            else
            {
                Debug.LogWarning("Teleport failed: Hit object does not have a NavMeshAgent.");
            }
        }
        else
        {
            Debug.LogWarning("Teleport failed: No valid target within range.");
        }
    }
}
