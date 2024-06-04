using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelfHeal : MonoBehaviour
{
    public PlayerStats playerStats;
    public HealthManager healthManager;
    [SerializeField] float interval;
    // Start is called before the first frame update
    void Start()
    {
        playerStats = GetComponent<PlayerStats>();
        healthManager = GetComponent<HealthManager>();
        //StartCoroutine(HealUnit());
        SelfHealtUnit();
    }

    private void SelfHealtUnit()
    {
        healthManager.HealDamage(playerStats.currentHealthRegen);
        Debug.Log("heal unit");
        Invoke("SelfHealtUnit", interval);
    }
    // IEnumerator HealUnit()
    // {
    //     //playerStats.currentHealth += playerStats.currentHealthRegen;
    //     healthManager.HealDamage(playerStats.currentHealthRegen);
    //     yield return new WaitForSeconds(interval);
    // }
}
