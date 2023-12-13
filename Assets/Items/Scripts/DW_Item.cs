using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum Item
{
    NULL,Test, Test1, Test2, Test3, Test4, Test5, Test6, Test7, Test8, Test9,
};

public abstract class DW_Item : MonoBehaviour
{
    public Item m_Item;

    public Sprite m_Texture;

    public bool isStackable = true;

    public List<ClassName> ExeptionStack;

    public Vector2 m_Position;

    public string m_title;
    public string m_description;

    public bool isWeapon = false;
    public abstract void Use();

    public Item GetItem()
    {
        return m_Item;
    }
}
