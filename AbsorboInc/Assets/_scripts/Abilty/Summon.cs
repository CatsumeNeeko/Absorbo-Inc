using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
[CreateAssetMenu(fileName ="Summon",menuName = "Ability/Summon")]
public class Summon : AbilitySO
{
    public GameObject summonedObject;
    public Transform summonLocation;
    //public float summonRange;
    public override void ActivateAbility(GameObject owner)
    {
        base.ActivateAbility(owner);

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if(Physics.Raycast(ray, out hit, abilityRange))
        {
            NavMeshHit navHit;
            if (NavMesh.SamplePosition(hit.point, out navHit, 0.1f, NavMesh.AllAreas))
            {
                // Instantiate the summoned object at the hit point
                GameObject summonedObj = Instantiate(summonedObject, navHit.position, Quaternion.identity);
                Debug.Log("Object summoned at: " + navHit.position);
            }
        }
    }
}
