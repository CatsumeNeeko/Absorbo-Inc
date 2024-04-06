using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullets : MonoBehaviour
{
    [SerializeField] float despawnTime;
    public float damage;
    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, despawnTime);
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            HealthManager playerHealth = other.GetComponent<HealthManager>();
            if (playerHealth != null)
            {
                playerHealth.TakeDamage(damage);
            }
            Destroy(gameObject);
        }
    }
}
