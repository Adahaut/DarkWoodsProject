using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class DW_Class : ScriptableObject
{
    // main informations
    public string className;
    public string classDescription;
    public Sprite classIcon;
    public DW_Passif classPassif;
    public DW_Skill classSkill;

    // stats
    public float currentHealth;
    public float maxHealth;
    private float minHealth = 0.0f;

    public float currentAttackSpeed;
    public float minattackDamage;
    public float maxattackDamage;

    // jauges spéciales ( en set qu'une seule )
    public float minPercentDamage;
    public float maxPercentDamage;
    [Range(0, 100)]
    public int amountHolyWater;
    [Range(0, 100)]
    public int amountAlcohol;
    [Range(0, 100)]
    public int amountFaith;

}
