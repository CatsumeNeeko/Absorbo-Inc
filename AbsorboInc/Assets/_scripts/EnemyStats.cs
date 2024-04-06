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

    [Header("RangedInfo")]
    public bool isRanged;
    public GameObject projectile;

    public float projectileSpeed;
    [Header("Visual Info")]
    public Material[] colourStates;
    public new Renderer renderer;
    [Header("StabilityInfo")]
    public float stabilityValue;
    public float stabilityChance;
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
        colourStates = enemyStats.colourStates;
        renderer = transform.GetChild(0).GetComponent<Renderer>();
        stabilityValue = enemyStats.stabilityValue;
        stabilityChance = enemyStats.stabilityChance;
    }
    public void Update()
    {
        
    }
    public void FixedUpdate()
    {
        SpecialFeatureCheck();
        DeathCheck();
    }

    void SpecialFeatureCheck()
    {
        if(enemyStats.hasFeatures)
        {
            enemyStats.UniqueFeature(gameObject);
            //Debug.Log("tHIS BE DONE");
        }
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
