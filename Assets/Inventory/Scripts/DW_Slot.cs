using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DW_Slot : MonoBehaviour
{
    public So_ItemData data;

    [SerializeField] private Image image;
    [SerializeField] private Sprite originalSprite;
    private Transform m_transform;


    DW_Interactions interactions;
    DW_Character player_character;


    void Awake()
    {
        m_transform = transform;
        image = gameObject.transform.parent.GetComponentInChildren<Image>();
        StockReset();
    }
    public void RefreshSlot()
    {
        if (data != null && image != null)
        {
            this.GetComponent<Image>().sprite = data.image;
        }
        else
        {
            this.GetComponent<Image>().sprite = originalSprite;
        }
    }

    public void Stock(DW_Item item)
    {
        StockLaw(item);
        image.sprite = item.m_Texture;
        data.image = item.m_Texture;
        data.item = item.m_Item;
        data.useAction = item.Use;
        data.numberOfItem++;
        data.consommable = !item.isWeapon;

    }



    public void Use()
    {
        if (data.useAction != null)
        {
            if ((data.item == Item.Key || data.item == Item.Consummable) && data.useAction())
            {
                data.numberOfItem--;
                Verification();
            }
        }
    }

    public Item GetStockItem()
    {
        return data.item;
    }


    private void StockLaw(DW_Item item)
    {
        if (data.item != item.m_Item || (!item.isStackable && !item.ExeptionStack.Contains(data._class)))
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
        image.sprite = null;
        data.image = null;
        data.item = Item.NULL;
        data.useAction = null;
        data.numberOfItem = 0;
    }

    private void Drop()
    {
        if(DW_DropController.Instance.Drop(data.item, Vector3.zero))
        {
            Debug.Log("b");
            data.numberOfItem--;
            Verification();
        }
    }

    private void OnMouseOver()
    {
        if(Input.GetMouseButtonDown(2)) 
        {
            Debug.Log("drop");
            Drop();
        }
    }

    public void SetClass(ClassName value) => data._class = value; 
}
