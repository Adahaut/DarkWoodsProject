using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class DW_ClassHolderRef : MonoBehaviour
{
    [Header("UI REFERENCES")]
    public Image classIcon;
    public Image skillIcon;
    public TextMeshProUGUI className;
    public Image healthBar;
    public Image specialBar;
    public DW_Slot slotLeft, slotRight;

    [HideInInspector] public DW_Class classRef;
    [SerializeField] private DW_ClassController class_controller;

    [SerializeField] private List<DW_Skill> all_skills;

    public void InitalizeCard(DW_Class class_ref)
    {
        

        classIcon.sprite = class_ref.classIcon;
        skillIcon.sprite = class_ref.classSkill.skillIcon;
        className.text = class_ref.className;
        healthBar.fillAmount = class_ref.currentHealth / class_ref.maxHealth;

        classRef = class_ref;

        slotLeft.data = class_ref.slotLeft;
        slotRight.data = class_ref.slotRight;

        slotLeft.RefreshSlot();
        slotRight.RefreshSlot();
        skillIcon.transform.GetChild(0).GetComponent<Button>().onClick.AddListener(() => { DW_GM_Classes.Instance.UseOtherSkill(this); });

        UpdateHealthBar();
        UpdateSpecialBar();
    }

    private void Update()
    {
        if( all_skills.Any(b => b.isOnCooldown == true))
        {
            foreach(var skill in all_skills)
            {
                if(skill.isOnCooldown)
                {
                    if (skill.currentSkillCooldown <= 0)
                    {

                        switch (skill.skillType)
                        {
                            case SkillType.Heal: break;
                            case SkillType.Attention:
                                class_controller.ResetAggro();
                                skill.isOnCooldown = false;
                                break;
                            case SkillType.Brazier: break;
                            case SkillType.Discretion: break;
                        }
                        skill.currentSkillCooldown = skill.skillCooldown;
                        skill.isOnCooldown = false;
                        continue;
                    }
                    else
                    {
                        skill.currentSkillCooldown -= Time.deltaTime;
                    }
                }
            }
        }
    }

    public void UpdateHealthBar()
    {
        healthBar.fillAmount = classRef.currentHealth / classRef.maxHealth;
    }

    public void UpdateSpecialBar()
    {
        switch (classRef.specialSourceType)
        {
            case specialSourceType.HolyWater:
                specialBar.fillAmount = classRef.specialSourceAmount / 100;
                break;
            case specialSourceType.Faith:
                specialBar.fillAmount = classRef.specialSourceAmount / 100;
                break;
            case specialSourceType.Alcohol:
                specialBar.fillAmount = classRef.specialSourceAmount / 100;
                break;
            case specialSourceType.None:
                specialBar.gameObject.SetActive(false);
                break;
        }
    }
}
