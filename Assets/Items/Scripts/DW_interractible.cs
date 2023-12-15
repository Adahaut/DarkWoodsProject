using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DW_interractible : DW_Item
{

    public override void Use()
    {
        Debug.Log("interractible");
        GameObject.FindAnyObjectByType<DW_Interactions>().Interact(this);
        // in player attack call interractible function. 
    }
}
