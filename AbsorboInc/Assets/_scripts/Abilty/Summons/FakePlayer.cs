using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FakePlayer : MonoBehaviour
{
    private Vector3 destination;
    private bool isMoving = false;
    public float moveSpeed;
    public int hitCount;
    public int hitMax;
    public void SetDestination(Vector3 target)
    {
        destination = target;
        isMoving = true;
    }
    private void Update()
    {
        if (isMoving)
        {
            MoveToDestination();
        }
        if(hitCount >= hitMax)
        {
            ResetPlayerTag();
        }
    }
    private void MoveToDestination()
    {
        // Calculate the direction to move towards
        Vector3 direction = (destination - transform.position).normalized;

        // Calculate the distance to the destination
        float distance = Vector3.Distance(transform.position, destination);

        // If the fake player is not at the destination yet, move towards it
        if (distance > 0.1f)
        {
            transform.position += direction * Time.deltaTime * moveSpeed;
        }
        else
        {
            // Stop moving when the destination is reached
            isMoving = false;
        }
    }
    private void ResetPlayerTag()
    {
        GameObject playerRef = GameObject.FindGameObjectWithTag("FakePlayer");
        playerRef.tag = "Player";
        Destroy(gameObject);
    }
}
