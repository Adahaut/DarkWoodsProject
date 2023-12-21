using System.Collections.Generic;
using UnityEngine;

// Use this script to be able to use abilities.
public class DW_ClassController : MonoBehaviour
{
    private static DW_ClassController instance;
    public static DW_ClassController Instance => instance;
    public DW_Class currentClass;
    public List<DW_Class> classes;
    [SerializeField] private DW_ClassHolderRef cardHolderRef;
    [SerializeField] private Movement controller_movement;

    private List<BehaviorTree_script> enemies_in_range = new List<BehaviorTree_script>();

    // effects that alterate the controller stats
    private bool is_paladin_aggro = false;
    private DW_Class current_aggro_class;
    public bool is_speed_reduced = false;
    public float speedReducedTimer = 0.0f;

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
                foreach(BehaviorTree_script enemy in enemies_in_range)
                {
                    Debug.Log(enemy.name);
                    enemy.view_distance += 10;
                    continue;
                }

                enemies_in_range.Clear();
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
        foreach(BehaviorTree_script enemy in GameObject.FindObjectsOfType<BehaviorTree_script>()) // change the script, this one is for tests
        {
            if(Vector3.Distance(this.transform.position, enemy.transform.position) < 50) // change the value, this one is for tests
            {
                enemy.view_distance -= 10;
                enemies_in_range.Add(enemy);
            }
        }
        speedReducedTimer = _abilityToUse.effectDuration;
        is_speed_reduced = true;
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
