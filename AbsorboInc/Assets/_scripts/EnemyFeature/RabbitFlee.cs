using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using static UnityEngine.GraphicsBuffer;
using static UnityEngine.RuleTile.TilingRuleOutput;
[CreateAssetMenu(fileName ="Rabbit", menuName ="Enemy/Rabbit")]
public class RabbitFlee : EnemiesSo
{
    public bool hasFlee;
    public float fleeThreshold;
    private Vector3 fleeLocation;
    public bool saveLoc = false;
    public override void UniqueFeature(GameObject owner)
    {
        base.UniqueFeature(owner);


        if(saveLoc == false)
        {
            fleeLocation = owner.transform.position;
            Debug.Log(fleeLocation);
            saveLoc = true;
        }

        EnemyStats enemystats = owner.GetComponent<EnemyStats>();
        
        float healthPercent = enemystats.currentHealth / enemystats.maxHealth;

        if(healthPercent <= fleeThreshold && hasFlee == false )
        {
            Debug.Log("Flee!!");
            NavMeshAgent navMeshAgent = owner.GetComponent<NavMeshAgent>();
            navMeshAgent.SetDestination(fleeLocation);
        }
    }

}
