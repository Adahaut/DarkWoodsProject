using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum SkillType
{
    None,
    Brazier,
    Heal,
    Discretion,
    Attention
}
[CreateAssetMenu]
public class DW_Skill : ScriptableObject
{
    // Skill visualisation
    public Sprite skillIcon;
    public SkillType skillType;
    public string skillName;
    public string skillDescription;

    // Skill management
    [Range(0, 100)]
    public int percentCost; // %
    public float skillCooldown; // secondes
    public float currentSkillCooldown; // secondes
    public bool isOnCooldown = false;

    // Visualisation
    public GameObject emitterVFX;

    // effects
    public float effectDuration;
    public float restaurationHealth;
}
