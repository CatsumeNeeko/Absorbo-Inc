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
    [SerializeField] Image AbilityOne, AbilityTwo, ConsumeAbility;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        StabilityUi();
        HealthBarUI();
        ResourceBarUI();
        AbilityUi();
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
    }
}
