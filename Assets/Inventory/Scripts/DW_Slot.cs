using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class DW_Slot : MonoBehaviour
{
    [SerializeField] private So_ItemData data;
    private UnityEngine.UI.Image image;
    private Transform m_transform;
    // Start is called before the first frame update
    void Start()
    {
        m_transform = transform;
        image = gameObject.transform.GetComponentInChildren<UnityEngine.UI.Image>();
    }

    public void Stock(DW_Item item)
    {
        StockLaw(item);
        data.image = item.m_Texture;
        data.item = item.m_Item;
        data.useAction = item.Use;
        data.numberOfItem++;

    }

    public void Use()
    {
        Debug.Log("Use");
        if (data.useAction != null)
        {
            data.useAction();
            data.numberOfItem--;
            Verification();
        }
    }

    public Item GetStockItem()
    {
        return data.item;
    }


    private void StockLaw(DW_Item item)
    {
        if (data.item != item.m_Item /*|| (!item.isStackable && !item.ExeptionStack.Contains(data._class))*/)
        {
            int num = data.numberOfItem;
            for (int i = 0; i < num; i++)
            {
                Drop();
            }
            StockReset();
        }
    }

    private void Verification()
    {
        if(data.numberOfItem<=0)
        {
            StockReset();
        }
    }

    private void StockReset()
    {
        data.image = null;
        data.item = Item.NULL;
        data.useAction = null;
        data.numberOfItem = 0;
    }

    private void Drop()
    {
        Debug.Log("a");
        if(DW_DropController.Instance.Drop(data.item, Vector3.zero))
        {
            Debug.Log("b");
            data.numberOfItem--;
            Verification();
        }
    }

    private void OnMouseOver()
    {
        if(Input.GetMouseButtonDown(0)) 
        {
            Drop();
        }
    }

    public void SetClass(TestClass value) => data._class = value; 
}
