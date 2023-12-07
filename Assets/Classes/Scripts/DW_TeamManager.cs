using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DW_TeamManager : MonoBehaviour
{
    // repasser en private après les tests
    public List<DW_Class> classes_selected = new List<DW_Class>();

    [Range(1, 4)]
    public int maxClasses;
    public bool isTeamFull = false;
    public Image buttonImage;

    public Canvas SelectionMenu;

    public void AddClassToTeam(DW_Class cls)
    {
        if(!isTeamFull && !classes_selected.Contains(cls))
        {
            Debug.Log(cls.className);
            classes_selected.Add(cls);

            // verifier ici si c'est == 3 
            // si c'est =3 dans ce cas le bouton jouer est mit en vert
            if(classes_selected.Count == maxClasses)
            {
                buttonImage.color = Color.green;
                isTeamFull = true;
            }
        }
    }

    public void RemoveClassFromTeam(DW_Class cls)
    {
        if(classes_selected.Contains(cls))
        {
            classes_selected.Remove(cls);
            // repasser le bouton jouer en rouge si le nombre de classes est inferieur a 3

            if (classes_selected.Count < maxClasses)
            {
                buttonImage.color = Color.white;
                isTeamFull = false;
            }
        }
    }

    public void TryStartGame()
    {
        if(isTeamFull)
        {
            //initialiser ici toutes les cartes avant
            Destroy(SelectionMenu.gameObject);
            Destroy(this.gameObject);
        }
    }
}
