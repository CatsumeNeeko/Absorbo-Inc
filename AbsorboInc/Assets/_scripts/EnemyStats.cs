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
    public bool isRanged;
    public GameObject projectile;
    public float projectileSpeed;

    private void Awake()
    {
        //Max/Min Stats
        maxHealth = enemyStats.baseMaxHealth;
        maxDefense = enemyStats.baseDefence;
        maxMovementSpeed = enemyStats.baseMovementSpeed;
        minMovementSpeed = enemyStats.baseMinimumMovementSpeed;
        //Current Stats
        currentHealth = maxHealth;
        currentDefense = maxDefense;
        currentMovementSpeed = maxMovementSpeed;
        //other stats
        attackCD = enemyStats.attackCooldown;
        attackRange = enemyStats.attackRange;
        damage = enemyStats.damage;
        defPenetration = enemyStats.defPenetration;
        enemyID = enemyStats.enemyID;
        isRanged = enemyStats.isRanged;
        if(enemyStats.projectile != null)
        {
            projectile = enemyStats.projectile;
            projectileSpeed = enemyStats.baseProjectileSpeed;
        }
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
