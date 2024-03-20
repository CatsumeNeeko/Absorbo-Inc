using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Resource
{
    None,
    Energy,
    Health
}
[CreateAssetMenu(fileName ="playableCharacter",menuName = "Character")]
public class PlayerCharacterStatsSo : ScriptableObject
{
    [Header("CharacterInfo")]
    public new string name;
    public GameObject model;
    [Header("ResourceStats")]
    public float baseMaxHealth;
    public float baseHealthRegen;
    public Resource resourceType;
    public float baseMaxResoruce;
    public float baseResourceRegen;
    [Header("Survival")]
    public float baseMovementSpeed;
    public float baseMinimumMovementSpeed;
    public float baseDefence;
    public float attackRange;
    public float baseAttackSpeed;
    public float baseMinAttackSpeed;
    public float baseConsumeTimer;
    public float baseAutoTimer;
    public float baseAutoDamage;
}
