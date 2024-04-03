using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
[CreateAssetMenu(fileName = "Commander",menuName ="Ability/Commander")]
public class Commander : AbilitySO
{
    public GameObject summonedObject;
    public Transform summonLocation;
    public float summonRange;

    public bool unitSummoned;
    public GameObject unitObject;
    public override void ActivateAbility(GameObject owner)
    {
        base.ActivateAbility(owner);


        if(!unitSummoned)
        {
            SpawnUnit();
        }
        else
        {
            UnitInteraction();
        }
    }
    public void SpawnUnit()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, summonRange))
        {
            NavMeshHit navHit;
            if (NavMesh.SamplePosition(hit.point, out navHit, 0.1f, NavMesh.AllAreas))
            {
                // Instantiate the summoned object at the hit point
                GameObject summonedObj = Instantiate(summonedObject, navHit.position, Quaternion.identity);
                Debug.Log("Object summoned at: " + navHit.position);
                unitObject = summonedObj;
            }
        }
    }
    public void UnitInteraction()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            string objectTag = hit.collider.tag;

            switch(objectTag)
            {
                case "Ground":
                    MoveUnit(hit);
                    break;
                case "Enemy":
                    AttackTarget(hit.collider.gameObject);
                    break;

            }
        }
    }
    public void MoveUnit(RaycastHit hit)
    {
        NavMeshAgent agent = unitObject.GetComponent<NavMeshAgent>();
        if (agent != null)
        {
            agent.SetDestination(hit.point);
        }
    }
    public void AttackTarget(GameObject enemy)
    {

    }
}
