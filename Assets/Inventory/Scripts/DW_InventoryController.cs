using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class DW_InventoryController : MonoBehaviour
{
    public static DW_InventoryController Instance;

    public Camera _camera;
    public DW_Item go;
    private Transform m_transform;
    [SerializeField] private List<DW_PersoInventory> personnageInventory = new List<DW_PersoInventory>();
    private Vector3 mouse_position = Vector3.zero;
    private bool isDroping = false;
    // Start is called before the first frame update
    void Awake()
    {
        if (Instance == null) { Instance = this; }

        //_camera = Camera.main;
        m_transform = transform;
        FoundInventory();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Z))
        {
            PickUp(go);
        }
        if(isDroping) 
        {

        }
        
    }


    public void PickUp(DW_Item Item)
    {
        if (TryStackByObject(Item))
        {
            Debug.Log("Is Stack By Obj");
            return;
        }
        else if (TryStack(Item))
        {
            Debug.Log("Is Stack");
            return;
        }
        else
            Debug.Log("Isn't Stack");
    }

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

    private void FoundInventory()
    {
        for(int i = 0; i < m_transform.childCount; i++)
        {
            personnageInventory.Add(m_transform.GetChild(i).GetComponent<DW_PersoInventory>());
        }
    }

    public void GetMousePos(InputAction.CallbackContext context)
    {
        Vector2 pos = context.ReadValue<Vector2>();
        mouse_position = pos;
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
