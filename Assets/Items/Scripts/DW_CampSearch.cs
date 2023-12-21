using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DW_CampSearch : MonoBehaviour
{
    public GameObject[] items;
    public bool hasBeenSearched = false;

    public DW_Item SearchCamp()
    {
        if(!hasBeenSearched)
        {
            hasBeenSearched = true;
            int index = Random.Range(0, items.Length);
            DW_Item item = items[index].GetComponent<DW_Item>();
            return item;
        }
        return null;
    }
}
