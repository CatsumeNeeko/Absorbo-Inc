using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="EnemyCharacter",menuName ="Enemy")]
public class EnemiesSo : ScriptableObject
{
    [Header("CharacterInfo")]
    public new string name;
    public GameObject model;
    public int enemyID;
    public bool isRanged;
    [Header("ResourceStats")]
    public float baseMaxHealth;
    public float damage;
    public float defPenetration;
    [Header("Survival")]
    public float baseMovementSpeed;
    public float baseMinimumMovementSpeed;
    public float baseDefence;
    public float attackRange;
    public float attackCooldown;
    public bool selfHeal;
    [Header("VisualInfo")]
    public int healthStates;
    public Color[] colourStates;
}
