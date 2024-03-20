using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName ="Last Stand",menuName ="Ability/Last Stand")]
public class LastStandAbility : AbilitySO
{
    public float healthThresholdPercentage = 0.3f; // 30%
    public int healthRefillAmount = 60; // Amount of health to refill
    public bool lastStandUsed = false;
    public override void ActivateAbility(GameObject user)
    {
        // Assuming PlayerHealth is a component attached to the player GameObject
        PlayerStats playerHealth = user.GetComponent<PlayerStats>();
        HealthManager healthManager = user.GetComponent<HealthManager>();
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
