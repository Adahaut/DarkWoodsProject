using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class DW_PersoInventory : MonoBehaviour
{
    [SerializeField] private List<DW_Slot> slots = new List<DW_Slot>();

    // Start is called before the first frame update
    void Awake()
    {
        FoundSlot();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public bool Stack(DW_Item item) 
    {
        for(int i = 0; i < slots.Count; i++) 
        {
            if (slots[i].GetStockItem() == Item.NULL || slots[i].GetStockItem() == item.m_Item)
            {
                slots[i].Stock(item);
                return true;
            }
        }
        

        return false;
    }

    private void FoundSlot()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            slots.Add(transform.GetChild(i).GetComponent<DW_Slot>());
        }
    }

}
