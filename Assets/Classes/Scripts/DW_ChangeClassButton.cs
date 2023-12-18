using UnityEngine;
using UnityEngine.UI;

public class DW_ChangeClassButton : MonoBehaviour
{
    [SerializeField] private int index_class;
    [SerializeField] private DW_TeamManager team_manager;
    private Button button;

    private void OnEnable()
    {
        DW_GM_Classes gm = DW_GM_Classes.Instance;
        this.GetComponent<Button>().onClick.AddListener(() => { gm.ApplySkill(team_manager.classes_selected[index_class]); }) ;
        button = this.GetComponent<Button>();
        gm.Death.AddListener(IsDead);
    }

    public void IsDead(DW_Class classRef)
    {
        if(classRef == team_manager.classes_selected[index_class])
        {
            button.interactable = false;
        }
    }
}
