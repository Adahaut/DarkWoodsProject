using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DW_ObjectDetection : MonoBehaviour
{
    public static DW_ObjectDetection Instance;

    public Vector2 playerPos = Vector2.zero; //Player Position on the grid (not the unity world position)

    [SerializeField] private List<DW_Item> objects = new List<DW_Item>(); // All object in the game
    [SerializeField]private List<DW_Item> current_object_arround = new List<DW_Item>(); // All object the player can take
    [SerializeField] private List<DW_Item> view_object_arround = new List<DW_Item>(); // All object in the pop-up
    [SerializeField] private List<GameObject> cards = new List<GameObject>();// All card in the pop-up

    public GameObject cardPrefab;

    private Transform m_transform;

    [SerializeField] private int current_card_select = 0;

    [SerializeField] private float distance; // Distance between the card and the center of the viewport
    [SerializeField] private float offset = -.12f; // Offset for have the select card in the center of the viewport
    
    public DW_Item currentObjectSelect;

    private void Awake()
    {
        m_transform = GetComponent<Transform>();
        if (Instance == null) { Instance = this; }
    }

    void Update()
    {
        IsTheObjectInMyArea();
    }

    //See if the object is near the player
    private void IsTheObjectInMyArea()
    {
        current_object_arround.Clear();
        for(int i = 0; i < objects.Count; i++) 
        {
            if (objects[i].m_Position == playerPos)
            {
                current_object_arround.Add(objects[i]);
            }
        }
        ShowObject();
    }

    // Put the new card of the object the player can take and delete the card the player can no longer take
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
                        //Selection();
                        continue;
                    }
                }
            }
        }
    }


    public void ClearObject()
    {
        objects.Clear();
    }

    public void AddObject(GameObject go)
    {
        objects.Add(go.GetComponent<DW_Item>());
    }

    public void RemoveObject(GameObject go) 
    {
        objects.Remove(go.GetComponent<DW_Item>());
    }

    public void SetPlayerPos(Vector2 pos)
    {
        playerPos = pos;
    }
}
