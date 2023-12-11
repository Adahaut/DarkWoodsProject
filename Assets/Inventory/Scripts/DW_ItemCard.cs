using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using TMPro;
using UnityEngine;

public class DW_ItemCard : MonoBehaviour
{
    [SerializeField] private DW_Item item;
    private TextMeshProUGUI title;
    private TextMeshProUGUI description;
    private Texture icon;

    private void Awake()
    {
        title = gameObject.transform.parent.GetChild(2).GetComponent<TextMeshProUGUI>();
        description = gameObject.transform.parent.GetChild(3).GetComponent<TextMeshProUGUI>();
        icon = gameObject.transform.parent.GetChild(4).GetComponent<Texture>();
    }
    // Start is called before the first frame update
    void Start()
    {
        title.text = item.m_title;
        description.text = item.m_description;
        icon = item.m_Texture;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    public void SetItem(DW_Item new_item) => item = new_item;
    public DW_Item GetItem() => item;
}
