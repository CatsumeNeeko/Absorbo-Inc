using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameUiMangaer : MonoBehaviour
{
    [Header("Dependencies")]
    [SerializeField] GameManager gameManager;
    [SerializeField] PlayerStats playerStats;

    [Header("SurvivalVariables")]
    [SerializeField] TMP_Text stabilityTimerText;
    [Header("HealthVariables")]
    [SerializeField] Image healthbarSprite;
    [SerializeField] TMP_Text healthbarText;
    private float target = 1f;
    private float reducedSpeed = 2f;
    public float maxHealth;
    public float currentHealth;
    [Header("ResourceVaraibles")]
    [SerializeField] Image ResourceBackground;
    [Header("AbilityVaraibles")]
    //[SerializeField] Image[] AbilityList;
    [SerializeField] Image AbilityOne, AbilityTwo,AbilityThree,ConsumeAbility;
    [SerializeField] TMP_Text AbilityOneTxt, AbilityTwoTxt, AbilityThreeTxt;
    [Header("StomachVariables")]
    [SerializeField] TMP_Text SlotZero;
    [SerializeField] TMP_Text SlotOne;
    [SerializeField] TMP_Text SlotTwo;
    [SerializeField] TMP_Text SlotThree;
    [SerializeField] TMP_Text SlotFour;
    [Header("ElevatorVariables")]
    [SerializeField] TMP_Text ElevatorText;
    [Header("ExtraStatsVariables")]
    [SerializeField] TMP_Text AtkDmgTxt;
    [SerializeField] TMP_Text MoveSpeedTxt;
    [SerializeField] Image AutoImage;
    void Start()
    {
        //GameObject player = GameObject.FindGameObjectWithTag("Player");
        //playerStats = player.GetComponent<PlayerStats>();
    }

    // Update is called once per frame
    void Update()
    {
        StabilityUi();
        HealthBarUI();
        ResourceBarUI();
        AbilityUi();
        StomachUi();
        ElevatorUi();
        ExtraStatsUi();
    }
    public void StabilityUi()
    {
        stabilityTimerText.text = gameManager.stabilityPercentage.ToString("F0");
    }
    public void HealthBarUI()
    {
        //healthbar movement
        maxHealth = playerStats.maxHealth;
        currentHealth = playerStats.currentHealth;

        target = currentHealth / maxHealth;
        healthbarSprite.fillAmount = Mathf.MoveTowards(healthbarSprite.fillAmount, target, reducedSpeed * Time.deltaTime);
        //healthbar Text
        healthbarText.text = currentHealth.ToString() + " / " + maxHealth.ToString();
    }
    public void ResourceBarUI()
    {
        if(playerStats.resourceType == Resource.None)
        {
            ResourceBackground.gameObject.SetActive(false);
        }
    }
    public void AbilityUi()
    {
        if(playerStats.canConsume == true)
        {
            ConsumeAbility.gameObject.SetActive(true);
        }
        else
        {
            ConsumeAbility.gameObject.SetActive(false);
        }


        if(playerStats.canAbilityOne == true)
        {
            AbilityOne.gameObject.SetActive(true);
        }
        else
        {
            AbilityOne.gameObject.SetActive(false);
        }


        if(playerStats.canAbilityTwo == true)
        {
            AbilityTwo.gameObject.SetActive(true);
        }
        else
        {
            AbilityTwo.gameObject.SetActive(false);
        }


        if(playerStats.canAbilityThree == true)
        {
            AbilityThree.gameObject.SetActive(true);
        }
        else
        {
            AbilityThree.gameObject.SetActive(false);
        }



        if (playerStats.abilities[0] != null)
        {
            AbilityOneTxt.text = playerStats.abilities[0].name;
        }
        else
            AbilityOneTxt.text = "";

        if (playerStats.abilities[1] != null)
        {
            AbilityTwoTxt.text = playerStats.abilities[1].name;
        }
        else
            AbilityTwoTxt.text = "";
        if (playerStats.abilities[2] != null)
        {
            AbilityThreeTxt.text = playerStats.abilities[2].name;
        }
        else
            AbilityThreeTxt.text = "";
    }
    public void StomachUi()
    {
        if (playerStats.stomachArray[0] != 0)
        {
            SlotZero.text ="Slot 1: "+ playerStats.stomachArray[0].ToString();
        }
        if (playerStats.stomachArray[1] != 0)
        {
            SlotOne.text ="Slot 2: "+ playerStats.stomachArray[1].ToString();
        }
        if (playerStats.stomachArray[2] != 0)
        {
            SlotTwo.text ="Slot 3: "+ playerStats.stomachArray[2].ToString();
        }
        if (playerStats.stomachArray[3] != 0)
        {
            SlotThree.text ="Slot 4: "+ playerStats.stomachArray[3].ToString();
        }
        if (playerStats.stomachArray[4] != 0)
        {
            SlotFour.text ="Slot 5: "+ playerStats.stomachArray[4].ToString();
        }
    }
    public void ElevatorUi()
    {
        int minutes = Mathf.FloorToInt(gameManager.escapeTimer / 60f);
        int seconds = Mathf.FloorToInt(gameManager.escapeTimer % 60f);

        ElevatorText.text = minutes.ToString("00") + " : " + seconds.ToString("00");
    }
    public void ExtraStatsUi()
    {
        if (playerStats.canAuto)
        {
            AutoImage.color = Color.green;
        }
        else
            AutoImage.color = Color.red;

        AtkDmgTxt.text = "ATK: " + playerStats.currentAutoDamage.ToString();
        MoveSpeedTxt.text = "SPD: " + playerStats.currentMovementSpeed.ToString();
    }
}
