using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

// Use this script to be able to use abilities.
public class DW_ClassController : MonoBehaviour
{
    private static DW_ClassController instance;
    public static DW_ClassController Instance => instance;
    public DW_Class currentClass;
    public List<DW_Class> classes;
    [SerializeField] private DW_ClassHolderRef cardHolderRef;

    // effects that alterate the controller stats
    private bool is_paladin_aggro = false;
    private DW_Class current_aggro_class;
    private bool is_speed_reduced = false;
    private float speedReducedTimer = 0.0f;
    private float removed_speed = 0.0f;

    private void Awake()
    {
        instance = this;
    }

    public void ChangeClass(DW_Class classChange)
    {
        if(classChange != currentClass)
        {
            if(!is_paladin_aggro)
            {
                currentClass.shouldBeAggro = false;
            }

            currentClass = classChange;

            if(!is_paladin_aggro) 
            {
                currentClass.shouldBeAggro = true;
            }

            this.GetComponent<DW_LifeManager>().OnChangeLeader(currentClass);
            cardHolderRef.InitalizeCard(classChange);
        }
    }
    private void Update()
    {
        //if (Input.GetKeyDown(KeyCode.P))
        //{
        //    UseAbility();
        //}

        if(is_speed_reduced)
        {
            if(speedReducedTimer <= 0)
            {
                // this.speed += removed_speed;
                removed_speed = 0.0f;
                speedReducedTimer = 0;
                is_speed_reduced = false;
                return;
            }
            speedReducedTimer -= Time.deltaTime;
        }
    }

    /*
     * Use the ability of the current class, if found.
     */
    public void UseAbility()
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

        Debug.Log(abilityToUse);
        if(abilityToUse != null)
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
        _abilityToUse.isOnCooldown = true;
        current_aggro_class = currentClass;
        currentClass.shouldBeAggro = true;
        is_paladin_aggro = true;
    }

    /*
     * Skill : Heal
     * Purpose : Heal a designated ally for 20% of his max health.
     */
    public void SkillHeal()
    {
        GameObject.FindObjectOfType<DW_GM_Classes>().UseHealSkill(FindClassHolderRef(currentClass));
    }

    /*
     * Skill : Discretion
     * Purpose : Reduce the enemies line of sight
     */
    public void SkillDiscretion(DW_Skill _abilityToUse)
    {
        foreach(DW_TestEnemyClass enemy in GameObject.FindObjectsOfType<DW_TestEnemyClass>()) // change the script, this one is for tests
        {
            if(Vector3.Distance(this.transform.position, enemy.transform.position) < 10) // change the value, this one is for tests
            {
                enemy.ReduceFov();
                speedReducedTimer = _abilityToUse.effectDuration;
                //removed_speed = (_abilityToUse.percentCost / 100) * this.speed
                // this.speed -= removed_speed;
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

    public void ResetAggro()
    {
        is_paladin_aggro = false;
        current_aggro_class.shouldBeAggro = false;
        current_aggro_class = null;
        currentClass.shouldBeAggro = true;
    }
}
