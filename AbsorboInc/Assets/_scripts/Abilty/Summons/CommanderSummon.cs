using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CommanderSummon : MonoBehaviour
{

    public float despawnTime;
    public float despawnTimer;


    void Update()
    {
        despawnTimer += Time.deltaTime;
        if (despawnTimer >= despawnTime)
        {
            Destroy(gameObject);
        }
    }
}
