using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Assertions.Must;
using UnityEngine.Events;

public class DW_GM_Classes : MonoBehaviour
{
    private static DW_GM_Classes instance;

    public static DW_GM_Classes Instance => instance;
    // organisation of the scene panels
    [SerializeField] private GameObject pre_game_environnement;
    [SerializeField] private GameObject in_game_environnement;

    [SerializeField] private Transform parent_inventory;
    [SerializeField] private DW_ClassHolderRef class_holder_ref;

    // skill management
    [SerializeField] private DW_TeamManager team_manager;

    private DW_Skill skill_to_use;
    private DW_ClassHolderRef initiator_skill;

    public UnityEvent<DW_Class> Death;

    private void Awake()
    {
        instance = this;

        if(Death == null) { Death = new UnityEvent<DW_Class>(); }
    }

    private void Start()
    {
        team_manager = GameObject.Find("TeamManager").GetComponent<DW_TeamManager>();
    }

    public void TryStartGame()
    {
        if(team_manager.isTeamFull)
        {
            
            pre_game_environnement.SetActive(false);
            in_game_environnement.SetActive(true);

            GameObject classCard = parent_inventory.GetChild(0).gameObject;

            classCard.GetComponent<DW_ClassHolderRef>().InitalizeCard(team_manager.classes_selected[0]);

            DW_ClassController.Instance.classes.Add(team_manager.classes_selected[0]);
            DW_ClassController.Instance.classes.Add(team_manager.classes_selected[1]);
            DW_ClassController.Instance.classes.Add(team_manager.classes_selected[2]);
            DW_ClassController.Instance.currentClass = team_manager.classes_selected[1];
            DW_ClassController.Instance.ChangeClass(team_manager.classes_selected[0]);

        }
    }

    public void UseOtherSkill(DW_ClassHolderRef classHolder)
    {
        DW_ClassController.Instance.UseAbility();
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

    public void ClassDeath(DW_Class classRef)
    {
        Death.Invoke(classRef);
        foreach (DW_Class c in DW_ClassController.Instance.classes)
        {

            if (c.currentHealth > 0)
            {
                ApplySkill(c);
                return;
            }
        }
    }

    public void ApplySkill(DW_Class classRef)
    {
        if(skill_to_use != null && initiator_skill != null)
        {
            DW_Class foundClass = classRef;
            foreach(DW_Class c in DW_ClassController.Instance.classes)
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
                    class_holder_ref.UpdateHealthBar();
                    classRef.classSkill.isOnCooldown = true;
                    break;
                }
            }
        }
        else
        {
            DW_ClassController.Instance.ChangeClass(classRef);
        }
        
        skill_to_use = null;
        initiator_skill = null;
    }
}
