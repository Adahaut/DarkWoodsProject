using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

// Use this script to be able to use abilities.
public class DW_ClassController : MonoBehaviour
{
    public DW_Class currentClass;
    public List<DW_Class> classes;

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            UseAbility();
            Debug.Log("ABILITY USAGE");
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

        if(currentClass.specialSourceAmount - abilityToUse.percentCost >= 0)
        {
            switch (abilityToUse.skillType)
            {
                case SkillType.Brazier:
                    SkillBrazier(abilityToUse);
                    break;
                default:
                    break;
            }
        }
    }
    public void SkillAttention()
    {

    }

    public void SkillHeal()
    {

    }
    public void SkillDiscretion()
    {

    }

    public void SkillBrazier(DW_Skill _abilityToUse)
    {
        GameObject ps = Instantiate(_abilityToUse.emitterVFX, this.transform.position + this.transform.forward, this.transform.rotation);
        currentClass.specialSourceAmount -= _abilityToUse.percentCost;
        ps.GetComponent<DW_ParticleSystemCollision>().ownerClass = currentClass;
        Debug.Log(currentClass.specialSourceAmount);
    }
}
