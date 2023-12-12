using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using static UnityEditor.Progress;

public class DW_PersoInventory : MonoBehaviour
{
    [SerializeField] private List<DW_Slot> slots = new List<DW_Slot>();
    [SerializeField] private ClassName m_class;

    void Awake()
    {
        FoundSlot();
    }


    public bool StackByObject(DW_Item item)
    {
        for (int i = 0; i < slots.Count; i++)
        {
            if (slots[i].GetStockItem() == item.m_Item && StockLaw(item))
            {
                slots[i].Stock(item);
                return true;
            }
        }


        return false;
    }

    public bool Stack(DW_Item item) 
    {
        for(int i = 0; i < slots.Count; i++) 
        {
            if (slots[i].GetStockItem() == Item.NULL)
            {
                slots[i].Stock(item);
                return true;
            }
        }
        

        return false;
    }


    private bool StockLaw(DW_Item item)
    {
        if (item.isStackable)
            return true;
        else if(item.ExeptionStack.Contains(m_class))
        {
            return true;
        }
        return false;
    }

    private void FoundSlot()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            DW_Slot slot = transform.GetChild(i).GetComponentInChildren<DW_Slot>();
           
            if(slot != null)
            {
                slots.Add(slot);
                slot.SetClass(m_class);
                Debug.Log("Slot found");
            }
            else
            {
                Debug.Log("Slot not found");
            }
        }
    }

}
