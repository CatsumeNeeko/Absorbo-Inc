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

    //public bool unitSummoned = false;
    //public GameObject unitObject = null;

    public override void ActivateAbility(GameObject owner)
    {
        base.ActivateAbility(owner);

        if(GameObject.FindGameObjectWithTag("CommanderSummon"))
        {
            UnitInteraction(owner);
            GameObject.FindGameObjectWithTag("CommanderSummon").GetComponent<CommanderSummon>().selectedTarget = null;
            //Debug.Log("Unit Interaction");
        }
        else
        {
            SpawnUnit(owner);
            //owner.GetComponent<MonoBehaviour>().StartCoroutine("ResetSpawn");
            //Debug.Log("Spawm Unit");
        }
    }
    public void SpawnUnit(GameObject owner)
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, summonRange))
        {
            Debug.Log(hit);
            NavMeshHit navHit;
            if (NavMesh.SamplePosition(hit.point, out navHit, 0.1f, NavMesh.AllAreas))
            {
               
                /*unitObject =*/ Instantiate(summonedObject, navHit.position, Quaternion.identity);
                Debug.Log("Object summoned at: " + navHit.position);
                //unitSummoned = true;
            }
        }
        else
        {
            Debug.Log("IF statment failed");
        }
    }
    public void UnitInteraction(GameObject owner)
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
        //NavMeshAgent agent = unitObject.GetComponent<NavMeshAgent>();
        NavMeshAgent agent = GameObject.FindGameObjectWithTag("CommanderSummon").GetComponent<NavMeshAgent>();
        if (agent != null)
        {
            agent.SetDestination(hit.point);
        }
    }
    public void AttackTarget(GameObject enemy)
    {
        GameObject.FindGameObjectWithTag("CommanderSummon").GetComponent<CommanderSummon>().SetTarget(enemy.transform);
    }
    
    
    //IEnumerator ResetSpawn()
    //{
    //    yield return new WaitForSeconds(20);
    //    unitSummoned = false;
    //}
}
