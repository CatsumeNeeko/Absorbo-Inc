using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStats : MonoBehaviour
{
    [Header("Dependancies")]
    public EnemiesSo enemyStats;

    [Header("Health Related Stats")]
    public float currentHealth;
    public float maxHealth;
    public float currentDefense;
    public float maxDefense;
    [Header("Surviaval Related Stats")]
    public float currentMovementSpeed;
    public float maxMovementSpeed;
    public float minMovementSpeed;
    public float damage;
    public float defPenetration;
    public float attackCD;
    public float attackRange;
    [Header("ScriptInfo")]
    public bool canHeal;
    public bool isDead;
    public int enemyID;

    private void Awake()
    {
        //Max/Min Stats
        maxHealth = enemyStats.baseMaxHealth;
        maxDefense = enemyStats.baseDefence;
        maxMovementSpeed = enemyStats.baseMovementSpeed;
        minMovementSpeed = enemyStats.baseMinimumMovementSpeed;
        attackCD = enemyStats.attackCooldown;
        attackRange = enemyStats.attackRange;
        damage = enemyStats.damage;
        defPenetration = enemyStats.defPenetration;
        enemyID = enemyStats.enemyID;
        //Current Stats
        currentHealth = maxHealth;
        currentDefense = maxDefense;
        currentMovementSpeed = maxMovementSpeed;
    }
    public void Update()
    {
        DeathCheck();
    }
    void DeathCheck()
    {
        if (isDead)
        {
            StartCoroutine("RemoveUnit");
        }
    }
    IEnumerator RemoveUnit()
    {
        yield return new WaitForSeconds(5);
        GameObject.Destroy(gameObject);
    }
}
