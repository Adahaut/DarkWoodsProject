using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DW_DropController : MonoBehaviour
{
    public static DW_DropController Instance;

    [SerializeField] private List<GameObject> m_Object;
    [SerializeField] Player player_pos;

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
                current_gameobject.transform.position = position;
                DW_ObjectDetection.Instance.AddObject(current_gameobject);
                return true;
            }
        }
        return false;
    }
}
