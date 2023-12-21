using System.Collections.Generic;
using UnityEngine;

public enum Item
{
    NULL,Key,Weapon, Consummable
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
    public abstract bool Use();

    public Sprite GetItem()
    {
        return m_Texture;
    }
}
