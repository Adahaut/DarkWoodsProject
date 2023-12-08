using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum Item
{
    NULL,Test, Test1
};

public abstract class DW_Item : MonoBehaviour
{
    public Item m_Item;
    public Texture m_Texture;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public abstract void Use();

    public Item GetItem()
    {
        return m_Item;
    }
}
