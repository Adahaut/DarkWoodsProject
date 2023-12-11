using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DW_ObjectDetection : MonoBehaviour
{
    public Vector2 playerPos = Vector2.zero;
    public static DW_ObjectDetection Instance;
    [SerializeField] private List<DW_Item> objects = new List<DW_Item>();
    [SerializeField]private List<DW_Item> current_object_arround = new List<DW_Item>();
    [SerializeField] private List<DW_Item> view_object_arround = new List<DW_Item>();
    [SerializeField] private List<GameObject> cards = new List<GameObject>();
    public GameObject cardPrefab;
    private Transform m_transform;
    [SerializeField] private int current_card_select = 0;
    [SerializeField] private float distance;
    [SerializeField] private float offset = -.12f;
    public DW_Item currentObjectSelect;

    private void Awake()
    {
        m_transform = GetComponent<Transform>();
        if (Instance == null) { Instance = this; }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        IsTheObjectInMyArea();
        if(Input.GetKeyDown(KeyCode.DownArrow)) 
        {
            cards[current_card_select].transform.GetChild(0).gameObject.SetActive(false);
            current_card_select++;
            Selection();
        }
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            cards[current_card_select].transform.GetChild(0).gameObject.SetActive(false);
            current_card_select--;
            Selection();
        }
        
    }

    private void IsTheObjectInMyArea()
    {
        current_object_arround.Clear();
        for(int i = 0; i < objects.Count; i++) 
        {
            if (Mathf.Abs(objects[i].m_Position.x - playerPos.x) <=1 && Mathf.Abs(objects[i].m_Position.y - playerPos.y) <= 1)
            {
                current_object_arround.Add(objects[i]);
            }
        }
        ShowObject();
    }

    private void Selection()
    {
        if (cards.Count > 0)
        {
            if (current_card_select >= cards.Count)
            {
                current_card_select = 0;
            }
            if (current_card_select < 0)
            {
                current_card_select = cards.Count - 1;
            }
            cards[current_card_select].transform.GetChild(0).gameObject.SetActive(true);
            currentObjectSelect = cards[current_card_select].GetComponentInChildren<DW_ItemCard>().GetItem();
            distance = m_transform.parent.position.y - cards[current_card_select].transform.position.y;
            m_transform.position = new Vector3(m_transform.position.x, m_transform.position.y + distance + offset, m_transform.position.z);
        }
        else
            currentObjectSelect = null;
    }

    private void ShowObject()
    {
        for(int i =0; i<current_object_arround.Count; i++) 
        {
            if (!view_object_arround.Contains(current_object_arround[i]))
            {
                view_object_arround.Add(current_object_arround[i]);
                GameObject card = Instantiate(cardPrefab, m_transform);
                card.GetComponentInChildren<DW_ItemCard>().SetItem(current_object_arround[i]);
                cards.Add(card);

            }
        }
        for(int i =0;i<view_object_arround.Count; i++)
        {
            if (!current_object_arround.Contains(view_object_arround[i]))
            {
                for(int j = 0; j< m_transform.childCount; j++) 
                {
                    if(m_transform.GetChild(j).gameObject.GetComponentInChildren<DW_ItemCard>().GetItem().gameObject == view_object_arround[i].gameObject)
                    {
                        cards.Remove(m_transform.GetChild(j).gameObject);
                        Destroy(m_transform.GetChild(j).gameObject);
                        view_object_arround.Remove(view_object_arround[i]);
                        current_card_select = 0;
                        Selection();
                        continue;
                    }
                }
            }
        }
    }


    public void AddObject(GameObject go)
    {
        objects.Add(go.GetComponent<DW_Item>());
    }
    public void RemoveObject(GameObject go) 
    {
        objects.Remove(go.GetComponent<DW_Item>());
    }
}
