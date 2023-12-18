using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DW_PladinStatue : MonoBehaviour
{
    public Vector2Int statue_paladin_pos;
    Vector2Int[] neighbors = { new Vector2Int(1, 0), new Vector2Int(-1, 0), new Vector2Int(0, 1), new Vector2Int(0, -1) };

    //continuously checks whether the player is in one of the squares next to the statue 
    public void Update()
    {
        foreach (var neighbor in neighbors)
        {
            Vector2Int possible_pos = statue_paladin_pos + neighbor;
            if (DW_GridMap.Instance.Grid[possible_pos.x, possible_pos.y] == 5)
            {
                if(DW_ClassController.Instance.currentClass.classType == ClassName.Paladin)
                {
                    if (DW_ClassController.Instance.currentClass.specialSourceAmount < 100)
                    {
                        DW_ClassController.Instance.currentClass.specialSourceAmount += 5 * Time.deltaTime;
                        Debug.Log(DW_ClassController.Instance.currentClass.specialSourceAmount);
                    }
                }
            }
        }
    }
}
