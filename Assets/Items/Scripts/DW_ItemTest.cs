using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DW_ItemTest : DW_Item
{
    void Start()
    {
        m_Item = Item.NULL;
    }

    public override bool Use()
    {
        Debug.Log("Use capacity Test");
        return true;
    }

}
