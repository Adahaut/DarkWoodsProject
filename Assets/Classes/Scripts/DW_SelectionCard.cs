using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DW_SelectionCard : MonoBehaviour
{
    public TextMeshProUGUI classNameText;
    public TextMeshProUGUI descriptionText;
    public TextMeshProUGUI healthText;
    public TextMeshProUGUI attackSpeedText;
    public TextMeshProUGUI attackDamageText;
    public TextMeshProUGUI specialAmountText;
    public Image iconPassif;
    public Image iconSkill;


    private void Start()
    {
        InitalizeCard();
    }

    // Update the card informations
    public void InitalizeCard()
    {
        DW_Class cardClass = this.transform.parent.GetComponent<DW_ButtonSelectionMenu>().typeOfClass;
        classNameText.text = cardClass.className;
        descriptionText.text = cardClass.classDescription;
        healthText.text = cardClass.currentHealth.ToString()+"/"+cardClass.maxHealth.ToString()+" hp";
        attackSpeedText.text = cardClass.currentAttackSpeed.ToString() + " a/s";
        attackDamageText.text = cardClass.minattackDamage.ToString() + "-" + cardClass.maxattackDamage.ToString() + " dmg";

        iconPassif.sprite = cardClass.classPassif.passifIcon;
        iconSkill.sprite = cardClass.classSkill.skillIcon;

        if (cardClass.amountFaith > 0)
            specialAmountText.text = cardClass.amountFaith.ToString() + "/100 faith";
        else if (cardClass.amountAlcohol > 0)
            specialAmountText.text = cardClass.amountAlcohol.ToString() + "/100 alcohol";
        else if (cardClass.amountHolyWater > 0)
            specialAmountText.text = cardClass.amountHolyWater.ToString() + "/100 holy water";
        else
            specialAmountText.text = "None";
    }
}
