using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DW_ButtonSelectionMenu : MonoBehaviour
{
    public DW_Class typeOfClass;
    public Color selectedColor;
    public Color originColor;

    private bool has_been_selected;
    private DW_TeamManager team_manager;

    private void Start()
    {
        this.GetComponent<Image>().color = originColor;
        team_manager = GameObject.Find("TeamManager").GetComponent<DW_TeamManager>();
    }

    public void OnButtonPressed()
    {
        if (has_been_selected)
        {
            has_been_selected = false;
            this.transform.Find("backgroundColor").GetComponent<Image>().color = originColor;
            team_manager.RemoveClassFromTeam(typeOfClass);
        }
        else
        {
            if (!team_manager.isTeamFull && !team_manager.classes_selected.Contains(typeOfClass))
            {
                has_been_selected = true;
                this.transform.Find("backgroundColor").GetComponent<Image>().color = selectedColor;
                team_manager.AddClassToTeam(typeOfClass);
            }
        }
    }
}
