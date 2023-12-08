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
        //Ray ray = _camera.ScreenPointToRay(mouse_position);


        //if(Physics.Raycast(ray, out RaycastHit hit)) 
        //{
        //    go.transform.position = hit.point;
        //}


        //Debug.Log("B" + _camera.ScreenToWorldPoint(mouse_position));
        //Debug.Log("A" + _camera.ScreenToWorldPoint(Input.mousePosition));
        
        //Debug.DrawRay(ray.origin, ray.direction*2000, Color.green);
    }


    public void PickUp(DW_Item Item)
    {
        if (personnageInventory[0].Stack(Item))
        {
            Debug.Log("Item Is Stack");
        }
        else
            Debug.Log("Item Isn't Stack");
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
