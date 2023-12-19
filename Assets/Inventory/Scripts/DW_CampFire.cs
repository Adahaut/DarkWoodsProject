using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DW_CampFire : MonoBehaviour
{
    // public GameObject refCampFire;
    public bool CampIsFree = true;
    public GameObject[] Item = { };
    public void CampFire()
    {
        RandomItem();
        CampIsFree = false;
        Debug.Log("CampsVisited");
    }

    public DW_Item RandomItem ()
    {
        if (CampIsFree)
        {
            int intRand = Random.Range(0, Item.Length - 1);
            var item = Instantiate(Item[intRand]);
            DW_Item i = item.GetComponent<DW_Item>() ;
            return i;
        }
        return null;
    }

}