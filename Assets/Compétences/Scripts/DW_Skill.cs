using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum SkillType
{
    None,
    Brazier,
    Heal
}
[CreateAssetMenu]
public class DW_Skill : ScriptableObject
{
    public Sprite skillIcon;
    public SkillType skillType;
    public string skillName;
    public string skillDescription;

    [Range(0, 100)]
    public int percentCost;

    public GameObject emitterVFX;
    public float restaurationHealth;
}
