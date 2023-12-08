using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

// Use this script to be able to use abilities.
public class DW_ClassController : MonoBehaviour
{
    public DW_Class currentClass;
    public List<DW_Class> classes;

    private int index_class = 0;
    private bool is_paladin_aggro = false;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            UseAbility();
        }

        if (Input.GetMouseButtonDown(1))
        {
            if(!is_paladin_aggro)
            {
                currentClass.shouldBeAggro = false;
            }
            if(index_class + 1 < classes.Count)
            {
                index_class += 1;
                currentClass = classes[index_class];
            }
            else
            {
                index_class = 0;
                currentClass = classes[index_class];
            }
            if(!is_paladin_aggro)
                currentClass.shouldBeAggro = true;
        }
    }

    /*
     * Use the ability of the current class, if found.
     */
    private void UseAbility()
    {
        DW_Skill abilityToUse = null;

        switch(currentClass.classType)
        {
            case ClassName.Pyromaniac:
                abilityToUse = currentClass.classSkill;
                break;
            case ClassName.Priest:
                abilityToUse = currentClass.classSkill;
                break;
            case ClassName.Paladin:
                abilityToUse = currentClass.classSkill;
                break;
            case ClassName.Thief:
                abilityToUse = currentClass.classSkill;
                break;
            default:
                break;
        }

        if(currentClass.specialSourceAmount - abilityToUse.percentCost >= 0 && !abilityToUse.isOnCooldown)
        {
            switch (abilityToUse.skillType)
            {
                case SkillType.Brazier:
                    SkillBrazier(abilityToUse);
                    break;
                case SkillType.Heal:
                    SkillHeal();
                    break;
                case SkillType.Discretion:
                    SkillDiscretion(abilityToUse);
                    break;
                case SkillType.Attention:
                    SkillAttention(abilityToUse);
                    break;
                default:
                    break;
            }
        }
    }

    /* 
     * Skill : Attention
     * Purpose : Takes the aggro instead of the team leader.
     */
    public void SkillAttention(DW_Skill _abilityToUse)
    {
        Debug.Log("Attention");

        _abilityToUse.isOnCooldown = true;
        currentClass.shouldBeAggro = true;
        is_paladin_aggro = true;
    }

    /*
     * Skill : Heal
     * Purpose : Heal a designated ally for 20% of his max health.
     */
    public void SkillHeal()
    {
        GameObject.FindObjectOfType<DW_GM_Classes>().RememberSkill(FindClassHolderRef(currentClass));
    }

    /*
     * Skill : Discretion
     * Purpose : Reduce the enemies line of sight
     */
    public void SkillDiscretion(DW_Skill _abilityToUse)
    {
        Debug.Log("Discretion");
        foreach(DW_TestEnemyClass enemy in GameObject.FindObjectsOfType<DW_TestEnemyClass>()) // change the script, this one is for tests
        {
            if(Vector3.Distance(this.transform.position, enemy.transform.position) < 10) // change the value, this one is for tests
            {
                enemy.ReduceFov();
            }
        }
        _abilityToUse.isOnCooldown = true;
    }

    /* 
     * Skill : Brazier
     * Purpose : Throw fire particles that can light and damage anything in front of the character
     */
    public void SkillBrazier(DW_Skill _abilityToUse)
    {
        if (FindClassHolderRef(currentClass) != null)
        {
            DW_ClassHolderRef chr = FindClassHolderRef(currentClass);
            GameObject ps = Instantiate(_abilityToUse.emitterVFX, this.transform.position + this.transform.forward, this.transform.rotation);
            currentClass.specialSourceAmount -= _abilityToUse.percentCost;
            ps.GetComponent<DW_ParticleSystemCollision>().ownerClass = currentClass;

            chr.UpdateHealthBar();
            chr.UpdateSpecialBar();
            _abilityToUse.isOnCooldown = true;
        }
    }

    /*
     * Get a reference to a class holder that possess the same class than the current class.
     */
    public DW_ClassHolderRef FindClassHolderRef(DW_Class classRef)
    {
        foreach( DW_ClassHolderRef chr in GameObject.FindObjectsOfType<DW_ClassHolderRef>())
        {
            if(chr.classRef == classRef)
            {
                return chr;
            }
        }
        return null;
    }

    public void ResetAggro(DW_Class classAggro)
    {
        is_paladin_aggro = false;
        classAggro.shouldBeAggro = false;
        currentClass.shouldBeAggro = true;
    }
}
