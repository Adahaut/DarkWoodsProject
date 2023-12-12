using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DW_ItemTest : DW_Item
{
    void Start()
    {
        m_Item = Item.Test;
    }

    public override void Use()
    {
        Debug.Log("Use capacity Test");
    }

}
