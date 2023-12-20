using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public enum ClassName
{
    None,
    Priest,
    Paladin,
    Pyromaniac,
    Thief
}

public enum specialSourceType
{
    None,
    HolyWater,
    Alcohol,
    Faith
}

[CreateAssetMenu]
public class DW_Class : ScriptableObject
{
    // main informations
    public string className;
    public string classDescription;
    public Sprite classIcon;
    public DW_Passif classPassif;
    public DW_Skill classSkill;

    public ClassName classType;

    // stats
    public float currentHealth;
    public float maxHealth;
    private float minHealth = 0.0f;

    public float currentAttackSpeed;
    public float minattackDamage;
    public float maxattackDamage;
    public float currentDamage => (Random.Range(minattackDamage, maxattackDamage) * (currentPercentDamage / 100) );

    public bool shouldBeAggro = false;

    // jauges spéciales ( en set qu'une seule )
    public float minPercentDamage;
    public float maxPercentDamage;
    public float currentPercentDamage => minPercentDamage;

    public specialSourceType specialSourceType;
    [Range(0, 100)]
    public float specialSourceAmount;

    public So_ItemData slotLeft;
    public So_ItemData slotRight;
}
