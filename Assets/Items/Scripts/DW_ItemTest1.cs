using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DW_ItemTest1 : DW_Item
{
    public override bool Use()
    {
        Debug.Log("Test1");
        return true;
    }

}
