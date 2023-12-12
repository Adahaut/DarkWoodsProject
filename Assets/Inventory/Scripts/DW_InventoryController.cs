using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class DW_InventoryController : MonoBehaviour
{
    public static DW_InventoryController Instance;

    public Camera _camera;

    private Transform m_transform;

    [SerializeField] private List<DW_PersoInventory> personnageInventory = new List<DW_PersoInventory>();

    private bool isDroping = false;

    void Awake()
    {
        if (Instance == null) { Instance = this; }

        m_transform = transform;
        FoundInventory();
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Z))
        {
            PickUp(DW_ObjectDetection.Instance.currentObjectSelect) ;
        }

        //if(isDroping) 
        //{

        //}
        
    }

    //Put the object in the world in the inventory 
    public void PickUp(DW_Item Item)
    {
        if(Item == null)
        {
            Debug.Log("No select object");
            return;
        }
        if (TryStackByObject(Item))
        {
            DW_ObjectDetection.Instance.RemoveObject(Item.gameObject);
            Debug.Log("Is Stack By Obj");
            return;
        }
        else if (TryStack(Item))
        {
            DW_ObjectDetection.Instance.RemoveObject(Item.gameObject);
            Debug.Log("Is Stack");
            return;
        }
        else
            Debug.Log("Isn't Stack");
    }

    //Put the item in the inventory if this object is already in one character inventory
    private bool TryStackByObject(DW_Item Item) 
    {
        for (int i = 0; i < personnageInventory.Count; i++)
        {
            if (personnageInventory[i].StackByObject(Item))
            {
                return true;
            }

        }
        return false;
    }

    //Put the item in the inventory if at least one character inventory is empty
    private bool TryStack(DW_Item Item)
    {
        for (int i = 0; i < personnageInventory.Count; i++)
        {
            if (personnageInventory[i].Stack(Item))
            {
                return true;
            }

        }
        return false;
    }

    // found all inventory character in the game
    private void FoundInventory()
    {
        for(int i = 0; i < m_transform.childCount; i++)
        {
            personnageInventory.Add(m_transform.GetChild(i).GetComponent<DW_PersoInventory>());
        }
    }

    public void IsDrop(InputAction.CallbackContext context)
    {
        isDroping = context.performed;
    }

    public void OnUi(Item item)
    {
        Debug.Log("OnUi "+ item.ToString());
    }
}
