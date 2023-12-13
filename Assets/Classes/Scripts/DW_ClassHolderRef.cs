using System.Collections;
using System.Collections.Generic;
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

    public void InitalizeCard(DW_Class class_ref)
    {
        skillIcon.transform.GetChild(0).GetComponent<Button>().onClick.AddListener(() => { DW_GM_Classes.Instance.UseOtherSkill(this); });

        classIcon.sprite = class_ref.classIcon;
        skillIcon.sprite = class_ref.classSkill.skillIcon;
        className.text = class_ref.className;
        healthBar.fillAmount = class_ref.currentHealth / class_ref.maxHealth;

        classRef = class_ref;

        slotLeft.data = class_ref.slotLeft;
        slotRight.data = class_ref.slotRight;

        slotLeft.RefreshSlot();
        slotRight.RefreshSlot();

        UpdateHealthBar();
        UpdateSpecialBar();
    }

    private void Update()
    {
        if (!classRef.classSkill.isOnCooldown )
            return;
        if(classRef.classSkill.currentSkillCooldown <= 0)
        {
            classRef.classSkill.isOnCooldown = false;
            classRef.classSkill.currentSkillCooldown =Mathf.RoundToInt( classRef.classSkill.skillCooldown);

            if(classRef.classSkill.skillType == SkillType.Attention)
                GameObject.FindObjectOfType<DW_ClassController>().ResetAggro(classRef);
            return;
        }

        classRef.classSkill.currentSkillCooldown -= Time.deltaTime;
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
