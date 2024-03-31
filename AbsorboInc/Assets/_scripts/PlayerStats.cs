using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    [Header("Dependancies")]
    public PlayerCharacterStatsSo playerCharacterStats;
    public PlayerAbilityList playerAbilityList;
    public AbilitySO[] abilities; 
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
    public bool lockStomach = false;
    public float consumeTimer;

    [Header("AutoAttack Related Stats")]
    public bool canAuto = true;
    public float autoAttackTimer;
    public float currentAutoDamage;
    [Header("Ability Related Stats")]
    private float[] cooldowntimers;
    public bool canAbilityOne = true, canAbilityTwo = true ,canAbilityThree = true;
    public float abilityOneTimer , abilityTwoTimer , abilityThreeTimer ;

    private void Start()
    {
        //array
        abilities = new AbilitySO[3];
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
        //autoAttack ststs
        autoAttackTimer = playerCharacterStats.baseAutoTimer;
        currentAutoDamage = playerCharacterStats.baseAutoDamage;
        //Ability Stats
        cooldowntimers = new float[abilities.Length];
    }
    public void FixedUpdate()
    {
        StomachSearch();
        PassiveCheck();
        //UpdateAbiltyCoolDowns();
    }
    #region Stomach
    public void StomachSearch()
    {
        if(!lockStomach)
        {
            int[] valueCount = new int[11];// the 11 is just a base estimate of the ammoumnt of enemeies being 10* 0 is null 

            foreach (int value in stomachArray)
            {
                valueCount[value]++;
            }

            for (int i = 0; i < valueCount.Length; i++)
            {
                if (valueCount[i] == 2)
                {
                    //Debug.Log("Two occurrences of value " + i);
                    StomachFilledTwo(i);
                }
                else if (valueCount[i] == 3)
                {
                    //Debug.Log("Three occurrences of value " + i);
                    StomachFilledThree(i);
                }
                else if (valueCount[i] == 5 && i != 0)
                {
                    //Debug.Log("Gene Mixed Complete");
                    lockStomach = true;
                    StomachFilledFive(i);
                }
            }
        }

    }

    void StomachFilledTwo(int value)
    {
        switch (value)
        {
            case 1:
                //Debug.Log("stomach filled two" + value);
                UpdateAbility(playerAbilityList.ID1_2);
                break;
            case 2:
                //Debug.Log("stomach filled two" + value);
                UpdateAbility(playerAbilityList.ID2_2);
                break;
            case 3:
                UpdateAbility(playerAbilityList.ID3_2);
                break;
        }
    }
    void StomachFilledThree(int value)
    {
        switch (value)
        {
            case 1:
                //Debug.Log("Stomach Filled Three" + value);
                UpdateAbility(playerAbilityList.ID1_3);
                break;
        }
    }
    void StomachFilledFive(int value)
    {
        switch(value)
        {
            case 1:
                SetFirstAbility(playerAbilityList.ID1_2);
                SetSecondAbility(playerAbilityList.ID1_3);
                break;
        }
    }
    #endregion
    #region AbilitySystem
    public void SetFirstAbility(AbilitySO newAbility)
    {
        abilities[0] = newAbility;
    }

    // Set the second ability
    public void SetSecondAbility(AbilitySO newAbility)
    {
        abilities[1] = newAbility;
    }
    public void UpdateAbility(AbilitySO newAbility)
    {
        bool abilityListFull = true;
        for(int i = 0; i< abilities.Length; i++)
        {
            if (abilities[i] == null)
            {
                abilityListFull = false;
                break;
            }
        }
        bool abilityInList = false;
        for(int i = 0 ; i< abilities.Length ; i++)
        {
            if (abilities[i] == newAbility)
            {
                abilityInList = true;
                break;
            }
        }
        if(abilityListFull && abilityInList == false)
        {
            for(int i = abilities.Length -/* 1 */2; i >= 0; i--)
            {
                //abilities[i] = abilities[i - 1];
                abilities[i + 1] = abilities[i];
            }
            abilities[0] = newAbility;
        }
        else if (abilityInList == false)
        {
            for (int i = 0; i < abilities.Length; i++)
            {
                if (abilities[i] == null)
                {
                    abilities[i] = newAbility;
                    break;
                }
            }
        }
        else if( abilityInList == true)
        {
            //Debug.Log(abilityInList);
        }
    }
    //Activate the first ability
    public void ActivateFirstAbility(GameObject user)
    {
        if (abilities[0] != null && canAbilityOne)
        {
            abilities[0].ActivateAbility(user);
            AbilityOneTimer();
        }
        else if(canAbilityOne == false)
        {
            Debug.Log("On Cooldown");
        }
        else
        {
            Debug.LogError("First ability not set.");
        }
        abilityOneTimer = abilities[0].cooldown;
    }

    // Activate the second ability
    public void ActivateSecondAbility(GameObject user)
    {
        if (abilities[1] != null && canAbilityTwo)
        {
            abilities[1].ActivateAbility(user);
            AbilityTwoTimer();
        }
        else if(canAbilityTwo == false)
        {
            Debug.Log("On Cooldown");
        }
        else
        {
            Debug.LogError("Second ability not set.");
        }
        abilityTwoTimer = abilities[1].cooldown;
    }
    //Activate the Third ability
    public void ActivateThirdAbility(GameObject user)
    {
        if (abilities[2] != null && canAbilityThree)
        {
            abilities[2].ActivateAbility(user);
            AbilityThreeTimer();
        }
        else if (canAbilityThree == false)
        {
            Debug.Log("On Cooldown");
        }
        else
        {
            Debug.LogError("Second ability not set.");
        }
        abilityThreeTimer = abilities[2].cooldown;
    }
    public void UpdateAbiltyCoolDowns()
    {
        if (abilities[0] != null)
        {
            abilityOneTimer = abilities[0].cooldown;
        }
        if (abilities[1] != null)
        {
            abilityTwoTimer = abilities[1].cooldown;
        }
        if (abilities[2] != null)
        {
            abilityThreeTimer= abilities[2].cooldown;
        }
    }
    public void PassiveCheck()
    {
        if (abilities[0] != null)
        {
            if (abilities[0].isPassive)
            {
                ActivateFirstAbility(gameObject);
                Debug.Log("Passive used" + abilities[0].name);
            }
        }
        if(abilities[1] != null)
        {
            if (abilities[1].isPassive)
            {
                ActivateSecondAbility(gameObject);
            }
        }
        if (abilities[2] != null)
        {
            if (abilities[2].isPassive)
            {
                ActivateThirdAbility(gameObject);
            }
        }
    }







    #endregion
    #region Timers
    /// <summary>
    /// The section below are used for timers 
    /// </summary>
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
    public void AbilityOneTimer()
    {
        if (canAbilityOne)
        {
            canAbilityOne = false;
            StartCoroutine("AbilityOneEnum");
        }
    }
    public void AbilityTwoTimer()
    {
        if (canAbilityTwo)
        {
            canAbilityTwo = false;
            StartCoroutine("AbilityTwoEnum");
        }
    }
    public void AbilityThreeTimer()
    {
        if (canAbilityThree)
        {
            canAbilityThree = false;
            StartCoroutine("AbilityThreeEnum");
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
    IEnumerator AbilityOneEnum()
    {
        yield return new WaitForSeconds(abilityOneTimer);
        canAbilityOne = true;
    }
    IEnumerator AbilityTwoEnum()
    {
        yield return new WaitForSeconds(abilityTwoTimer);
        canAbilityTwo = true;
    }
    IEnumerator AbilityThreeEnum()
    {
        yield return new WaitForSeconds(abilityThreeTimer);
    }
    #endregion
}
