using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.Rendering;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/Item")]
public class So_ItemData : ScriptableObject
{
    public Sprite image = null;
    public Item item = Item.NULL;
    public Action useAction = null;
    public int numberOfItem = 0;
    public TestClass _class = TestClass.NULL;
}
