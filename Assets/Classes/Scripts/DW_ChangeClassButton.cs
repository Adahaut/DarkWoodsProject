using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DW_ChangeClassButton : MonoBehaviour
{
    [SerializeField] private int index_class;
    [SerializeField] private DW_TeamManager team_manager;
    private void OnEnable()
    {
        DW_GM_Classes gm = DW_GM_Classes.Instance;
        this.GetComponent<Button>().onClick.AddListener(() => { gm.ApplySkill(team_manager.classes_selected[index_class]); }) ;
        
    }
}
