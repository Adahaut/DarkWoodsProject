using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class DW_ClassHolderRef : MonoBehaviour
{
    // what class is this refering to
    public DW_Class classRef;
    public DW_Skill classSkill;
    public DW_Passif classPassif;

    // set up all the card informations
    public Image classIcon;
    public Image skillIcon;
    public TextMeshProUGUI className;

    public void InitalizeCard()
    {
        DW_GM_Classes gm = GameObject.FindAnyObjectByType<DW_GM_Classes>();
        this.GetComponent<Button>().onClick.AddListener(() => { gm.ApplySkill(this); });
        skillIcon.transform.GetComponent<Button>().onClick.AddListener(() => { gm.RememberSkill(this); });

        classIcon.sprite = classRef.classIcon;
        skillIcon.sprite = classSkill.skillIcon;
        className.text = classRef.className;
    }
}
