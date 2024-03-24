using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAutoBullet : MonoBehaviour
{
    public PlayerStats playerStats;

    public Transform target;
    public Transform targetCheck;
    public Rigidbody rb;
    [SerializeField] float stoppingDistance;

    private void Start()
    {
        targetCheck = target;
        playerStats = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerStats>();
        rb = GetComponent<Rigidbody>();
    }
    private void Update()
    {
        if(target != null)
        {
            Vector3 direction = target.position - transform.position;
            rb.velocity = direction.normalized * 10f;
        }
        else
        {
            Debug.Log("target is null");
            Destroy(gameObject);
        }
    }
    public void SetTarget(Transform targetCheck)
    {
        target = targetCheck;
    }


    private void OnTriggerEnter(Collider other)
    {
        if (target != null && ReferenceEquals(other.gameObject, target.gameObject))
        {
            Debug.Log("Hit enemy");
            HealthManager health = other.GetComponent<HealthManager>();
            if (health != null)
            {
                health.TakeDamage(playerStats.currentAutoDamage);
                Debug.Log("Health Remaining" + health.enemyStats.currentHealth);
                Destroy(gameObject);
            }
        }

    }
    
}
