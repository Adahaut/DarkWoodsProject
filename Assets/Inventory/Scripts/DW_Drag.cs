using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class DW_Drag : MonoBehaviour
{
    public Camera _camera;

    public DW_Item _item;

    [SerializeField]private bool _isDragging = false;

    [SerializeField] private GameObject Model;
    [SerializeField] private GameObject current;

    private float distance = 200;

    [SerializeField] private LayerMask layer;

    private void Awake()
    {
        if (current == null) { current = Instantiate<GameObject>(Model); current.SetActive(false); }
    }
    private void OnMouseOver()
    {
        Debug.Log("blabla");
        if(Input.GetMouseButtonDown(1))
        {
            Debug.Log("Here");
            current.SetActive(true);
            _isDragging = true;
        }
        
    }

    private void Update()
    {
        Ray ray = _camera.ScreenPointToRay(Input.mousePosition);

        Debug.DrawRay(ray.origin, ray.direction*2000);

        if (Physics.Raycast(ray, out RaycastHit hit, 2000, layer))
        {
            Debug.Log(hit.collider.gameObject.name);
            // see if the player try to drag a card item in the pop_up
            if(hit.collider.CompareTag("UiItem") && !_isDragging && Input.GetMouseButtonDown(1) && DW_ViewPort.Instance.objectInViewPort.Contains(hit.collider.gameObject))
            {
                _item = hit.collider.gameObject.GetComponent<DW_ItemCard>().GetItem();
                current.SetActive(true);
                _isDragging =true;
            }
            // see if the player drop the card item on a slot 
            if(hit.collider.CompareTag("UiSlot") &&  _isDragging && Input.GetMouseButtonUp(1))
            {
                if(hit.collider.gameObject.TryGetComponent<DW_Slot>(out DW_Slot slot))
                {
                    slot.Stock(_item);
                    DW_ObjectDetection.Instance.RemoveObject(_item.gameObject);
                    _item.gameObject.SetActive(false);
                }
            }
        }

        // Show the drag object
        if(_isDragging)
        {
            current.transform.position = ray.GetPoint(distance);
        }


        if(Input.GetMouseButtonUp(1)) { _isDragging = false;  current.SetActive(false); }
    }

}
