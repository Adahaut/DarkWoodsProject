using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DW_consumable : DW_Item
{

    public float healthRestoreAmount;
    public float specialRestoreAmount;
    public override void Use()
    {
        Debug.Log("consumable");
        // in player attack call consumable function.
    }
}
