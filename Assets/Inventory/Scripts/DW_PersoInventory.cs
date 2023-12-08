using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using static UnityEditor.Progress;

public class DW_PersoInventory : MonoBehaviour
{
    [SerializeField] private List<DW_Slot> slots = new List<DW_Slot>();
    [SerializeField] private TestClass _class;

    // Start is called before the first frame update
    void Awake()
    {
        FoundSlot();
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public bool StackByObject(DW_Item item)
    {
        for (int i = 0; i < slots.Count; i++)
        {
            if (slots[i].GetStockItem() == item.m_Item)
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

    private void FoundSlot()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            if(transform.GetChild(i).TryGetComponent<DW_Slot>(out DW_Slot slot))
            {
                slots.Add(slot);
                slot.SetClass(_class);
            }

        }
    }

}
