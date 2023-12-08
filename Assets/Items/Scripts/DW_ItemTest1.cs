using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DW_ItemTest1 : DW_Item
{
    // Start is called before the first frame update
    void Start()
    {
        m_Item = Item.Test1;
    }

    public override void Use()
    {
        Debug.Log("Test1");
    }

    // Update is called once per frame
    void Update()
    {

    }
}
