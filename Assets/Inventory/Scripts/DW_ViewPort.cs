using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DW_ViewPort : MonoBehaviour
{
    public static DW_ViewPort Instance;

    private void Awake()
    {
        if(Instance == null) { Instance = this; }
    }

    public List<GameObject> objectInViewPort = new List<GameObject>();

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Enter in contact");

        if(other.gameObject.CompareTag("UiItem") && !objectInViewPort.Contains(other.gameObject)) 
        {
            objectInViewPort.Add(other.gameObject);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("UiItem") && objectInViewPort.Contains(other.gameObject))
        {
            objectInViewPort.Remove(other.gameObject);
        }
    }
}
