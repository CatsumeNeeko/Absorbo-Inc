using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    [Header("Dependancies")]
    public PlayerCharacterStatsSo playerCharacterStats;

    [Header("Health Related Stats")]
    public float currentHealth;
    public float maxHealth;
    public float currentHealthRegen;
    public float maxHealthRegen;
    public float currentDefense;
    public float maxDefense;
    [Header("Reasource Related Stats")]
    public Resource resourceType;
    public float currentResourceAmmount;
    public float maxResourceAmmount;
    public float resourceRegen;
    [Header("Surviaval Related Stats")]
    public float currentMovementSpeed;
    public float maxMovementSpeed;
    public float minMovementSpeed;

    [Header("Absorbtion Related Stats")]
    public int[] stomachArray;
    public bool canConsume = true;
    public float consumeTimer;

    [Header("AutoAttack Related Stats")]
    public bool canAuto = true;
    public float autoAttackTimer;


    private void Start()
    {
        //array
        stomachArray = new int[5];
        consumeTimer = playerCharacterStats.baseConsumeTimer;
        //Max/Min Stats
        maxHealth = playerCharacterStats.baseMaxHealth;
        maxDefense = playerCharacterStats.baseDefence;
        maxHealthRegen = playerCharacterStats.baseHealthRegen;
        maxResourceAmmount = playerCharacterStats.baseMaxResoruce;
        maxMovementSpeed = playerCharacterStats.baseMovementSpeed;
        minMovementSpeed = playerCharacterStats.baseMinimumMovementSpeed;
        //Current Stats
        currentHealth = maxHealth;
        currentDefense = maxDefense;
        currentHealthRegen = maxHealthRegen;
        currentResourceAmmount = maxResourceAmmount;
        resourceRegen = playerCharacterStats.baseResourceRegen;
        currentMovementSpeed = minMovementSpeed;
    }
    public void Update()
    {
        
    }

    public void ConsumeTimer()
    {
        if(canConsume)
        {
            canConsume = false;
            StartCoroutine("ConsumeEnum");
        }
    }
    public void AutoTimer()
    {
        if(canAuto)
        {
            canAuto = false;
            StartCoroutine("AutoEnum");
        }
    }
    IEnumerator ConsumeEnum()
    {
        yield return new WaitForSeconds(consumeTimer);
        canConsume = true;
    }
    IEnumerator AutoEnum()
    {
        yield return new WaitForSeconds(autoAttackTimer);
        canAuto = true;
    }
}
