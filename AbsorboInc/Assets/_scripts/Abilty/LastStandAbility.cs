using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName ="Last Stand",menuName ="Ability/Last Stand")]
public class LastStandAbility : AbilitySO
{
    public float healthThresholdPercentage = 0.3f; // 30%
    public int healthRefillAmount = 60; // Amount of health to refill
    public bool lastStandUsed = false;

    public override void DamageTook(GameObject owner, float damage)
    {
        base.DamageTook(owner, damage);

        PlayerStats playerHealth = owner.GetComponent<PlayerStats>();
        HealthManager healthManager = owner.GetComponent<HealthManager>();
        if (playerHealth != null)
        {
            Debug.Log(lastStandUsed);
            // Calculate the health threshold
            float healthThreshold = playerHealth.maxHealth * healthThresholdPercentage;
            // Check if current health is below the threshold

            if (playerHealth.currentHealth <= healthThreshold && lastStandUsed == false)
            {
                // Refill the player's health
                healthManager.HealDamage(healthRefillAmount);
                Debug.Log("Player's health was refilled.");
                lastStandUsed = true;
            }
        }
        else
        {
            Debug.LogWarning("PlayerHealth component not found on the player GameObject.");
        }
    }
}
