using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/SaveData")]

public class DW_Save : ScriptableObject
{
    public Vector2 checkpoint_position;
    [Range(0,1)]public int map_save; //0 = forest / 1 = hospital
    public List<DW_Class> data_class;
}
