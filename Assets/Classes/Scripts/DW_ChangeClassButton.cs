using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DW_ChangeClassButton : MonoBehaviour
{
    [SerializeField] private int indexClass;
    private void OnEnable()
    {
        Debug.Log("Button updated");
        DW_GM_Classes gm = GameObject.FindAnyObjectByType<DW_GM_Classes>();
        this.GetComponent<Button>().onClick.AddListener(() => { gm.ApplySkill(GameObject.FindAnyObjectByType<DW_TeamManager>().classes_selected[indexClass]); }) ;
        
    }
}
