using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum Item
{
    NULL,Test, Test1, Test2, Test3, Test4, Test5, Test6, Test7, Test8, Test9,
};

public enum TestClass
{
    NULL,Class1, Class2, Class3
};

public abstract class DW_Item : MonoBehaviour
{
    public Item m_Item;
    public Texture m_Texture;
    public bool isStackable = true;
    public List<TestClass> ExeptionStack;
    public Vector2 m_Position;
    public string m_title;
    public string m_description;
    
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
