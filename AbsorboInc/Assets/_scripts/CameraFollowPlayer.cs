using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollowPlayer : MonoBehaviour
{
    private string playerTag = "Player";
    public Transform target;
    public Vector3 offset;

    private void Start()
    {
        GameObject player = GameObject.FindGameObjectWithTag(playerTag);
        if (player != null)
        {
            target = player.transform;
        }
        else
            Debug.Log("Cant Find Player");
    }
    void LateUpdate()
    {
        if (target != null)
        {            
            transform.position = target.position + offset;
        }
    }
}
