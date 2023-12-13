using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Assertions.Must;

public class DW_GM_Classes : MonoBehaviour
{
    // organisation of the scene panels
    public GameObject preGameEnvironnement;
    public GameObject inGameEnvironnement;

    public Transform parentInventory;

    // skill management
    private DW_TeamManager team_manager;
    private DW_Skill skill_to_use;
    public DW_ClassHolderRef initiator_skill;

    private void Start()
    {
        team_manager = GameObject.Find("TeamManager").GetComponent<DW_TeamManager>();
    }

    public void TryStartGame()
    {
        if(team_manager.isTeamFull)
        {
            
            preGameEnvironnement.SetActive(false);
            inGameEnvironnement.SetActive(true);

            GameObject classCard = parentInventory.GetChild(0).gameObject;

            classCard.GetComponent<DW_ClassHolderRef>().InitalizeCard(team_manager.classes_selected[0]);

            GameObject.FindAnyObjectByType<DW_ClassController>().classes.Add(team_manager.classes_selected[0]);
            GameObject.FindAnyObjectByType<DW_ClassController>().classes.Add(team_manager.classes_selected[1]);
            GameObject.FindAnyObjectByType<DW_ClassController>().classes.Add(team_manager.classes_selected[2]);
            GameObject.FindAnyObjectByType<DW_ClassController>().currentClass = GameObject.FindAnyObjectByType<DW_ClassController>().classes[0];

        }
    }

    public void UseOtherSkill(DW_ClassHolderRef classHolder)
    {
        GameObject.FindAnyObjectByType<DW_ClassController>().UseAbility();
    }

    public void UseHealSkill(DW_ClassHolderRef classHolder)
    {
        DW_Skill skill = classHolder.classRef.classSkill;
        if(!skill_to_use && skill.skillType == SkillType.Heal)
        {
            initiator_skill = classHolder;
            skill_to_use = skill; 
        }
    }

    public void ApplySkill(DW_Class classRef)
    {
        if(skill_to_use != null && initiator_skill != null)
        {
            DW_Class foundClass = classRef;
            foreach(DW_Class c in GameObject.FindAnyObjectByType<DW_ClassController>().classes)
            {
                if (c == foundClass && initiator_skill.classRef.specialSourceAmount - skill_to_use.percentCost >= 0 && c.currentHealth < c.maxHealth)
                {
                    c.currentHealth += Mathf.RoundToInt( ((skill_to_use.restaurationHealth/100) * c.maxHealth));

                    if (c.currentHealth > c.maxHealth)
                    {
                        c.currentHealth = c.maxHealth;
                    }
                    initiator_skill.classRef.specialSourceAmount -= skill_to_use.percentCost;
                    initiator_skill.UpdateSpecialBar();
                    GameObject.FindAnyObjectByType<DW_ClassHolderRef>().UpdateHealthBar();
                    classRef.classSkill.isOnCooldown = true;
                    break;
                }
            }
        }
        else
        {
            GameObject.FindAnyObjectByType<DW_ClassController>().ChangeClass(classRef);
        }
        
        skill_to_use = null;
        initiator_skill = null;
    }
}
