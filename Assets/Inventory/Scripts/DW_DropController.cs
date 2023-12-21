using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DW_DropController : MonoBehaviour
{
    public static DW_DropController Instance;

    [SerializeField] private List<GameObject> m_Object;
    [SerializeField] DW_Character player_pos;
    [SerializeField] private Transform mapforest, mapHospital;

    private GameObject current_gameobject;

    private void Awake()
    {
        if (Instance == null) { Instance = this; }
    }


    // Drop the item in the world
    public bool Drop(Item item, Vector3 position)
    {
        position = player_pos.transform.position;
        for (int i = 0;  i < m_Object.Count; i++)
        {
            if (m_Object[i].GetComponent<DW_Item>().GetItem() == item)
            {
                current_gameobject = Instantiate<GameObject>(m_Object[i]);
                current_gameobject.transform.localPosition = new Vector3(position.x,0,position.z);
                current_gameobject.GetComponent<DW_Item>().m_Position = new Vector2(Mathf.Abs(position.x / 10), Mathf.Abs(position.z / 10));
                current_gameobject.transform.localScale = new Vector3(5,5,5);
                if(mapforest.gameObject.activeSelf)
                {
                    current_gameobject.transform.parent = mapforest;
                }
                else
                {
                    current_gameobject.transform.parent = mapHospital;
                }
                current_gameobject.SetActive(true);
                DW_ObjectDetection.Instance.AddObject(current_gameobject);
                Debug.Log("Successfuly dropped");
                return true;
            }
        }
        return false;
    }
}
