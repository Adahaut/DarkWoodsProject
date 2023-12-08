using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DW_DropController : MonoBehaviour
{
    public static DW_DropController Instance;

    [SerializeField] private List<GameObject> m_Object;

    private GameObject current_gameobject;

    private void Awake()
    {
        if (Instance == null) { Instance = this; }
    }


    public bool Drop(Item item, Vector3 position)
    {
        for(int i = 0;  i < m_Object.Count; i++)
        {
            if (m_Object[i].GetComponent<DW_Item>().GetItem() == item)
            {
                current_gameobject = Instantiate<GameObject>(m_Object[i]);
                current_gameobject.transform.position = position;
                return true;
            }
        }
        return false;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
