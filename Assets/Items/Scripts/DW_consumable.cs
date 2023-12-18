using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DW_consumable : DW_Item
{

    public float healthRestoreAmount;
    public float specialRestoreAmount;
    public specialSourceType specialSourceType;
    public override void Use()
    {
        GameObject.FindAnyObjectByType<DW_Interactions>().ConsumeItem(this, specialSourceType);
    }
}
