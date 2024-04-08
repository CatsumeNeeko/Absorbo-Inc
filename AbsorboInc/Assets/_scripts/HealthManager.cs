using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthManager : MonoBehaviour
{
    [Header("Dependencies")]
    public PlayerStats playerStats;
    public EnemyStats enemyStats;
    public EnemyMovement enemyMovement;
    [Header("Info")]
    [SerializeField] float damageReduction;




    public void TakeDamage(float damage)
    {
        if(playerStats != null)
        {
            for (int i = 0; i < playerStats.abilities.Length; i++)
            {
                if (playerStats.abilities[i] != null)
                {
                    playerStats.abilities[i].DamageTook(gameObject, damage);
                }
            }


            damageReduction = playerStats.currentDefense / 2;

            if (playerStats.currentHealth > 0)
            {
                playerStats.currentHealth -= damage / damageReduction;
                if (playerStats.currentHealth <= 0)
                {
                    Die();
                }
            }
        }
        else
        {
            damageReduction = enemyStats.currentDefense / 2;
            
            if(enemyStats.currentHealth > 0)
            {
                enemyStats.currentHealth -= damage / damageReduction;
                if(enemyStats.currentHealth <= 0)
                {
                    Die();
                }
                UpdateMaterial();
            }
        }
    }
    public void HealDamage(float heal)
    {
        playerStats.currentHealth += heal;
        if (playerStats.currentHealth > playerStats.maxHealth)
        {
            playerStats.currentHealth = playerStats.maxHealth;
        }
    }

    private void Die()
    {
        if(playerStats != null) { }
        else
        {
            enemyStats.isDead = true;
        }
    }

    void UpdateMaterial()
    {
        Debug.Log("UpdateMaterial");
        float healthPercentage = enemyStats.currentHealth / enemyStats.maxHealth;
        int healthIndex = Mathf.Clamp(Mathf.FloorToInt((1 - healthPercentage) * enemyStats.colourStates.Length), 0, enemyStats.colourStates.Length - 1);
        //int healthIndex = Mathf.Clamp(Mathf.FloorToInt(healthPercentage * enemyStats.colourStates.Length), 0, enemyStats.colourStates.Length - 1);

        enemyStats.renderer.material = enemyStats.colourStates[healthIndex];
    }

    public void Update()
    {
        //if (Input.GetKeyDown(KeyCode.Alpha1))
        //{
        //    TakeDamage(10f);
        //}
        //if (Input.GetKeyDown(KeyCode.Alpha2))
        //{
        //    HealDamage(10f);
        //}



    }
}
