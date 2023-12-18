using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DW_ChangeClassButton : MonoBehaviour
{
    [SerializeField] private int index_class;
    [SerializeField] private DW_TeamManager team_manager;
    [SerializeField] private bool Is_clickable;
    private Button button;

    private void OnEnable()
    {
        DW_GM_Classes gm = DW_GM_Classes.Instance;
        this.GetComponent<Button>().onClick.AddListener(() => { gm.ApplySkill(team_manager.classes_selected[index_class]); }) ;
        button = this.GetComponent<Button>();
    }

    private void Update()
    {
        if(!Is_clickable && DW_GM_Classes.Instance.IsClassAlive(team_manager.classes_selected[index_class])) 
        {
            button.interactable = true;
            Is_clickable = true;
        }
        else if(Is_clickable && !DW_GM_Classes.Instance.IsClassAlive(team_manager.classes_selected[index_class]))
        {
            button.interactable = false;
            Is_clickable = false;
        }
    }
}
