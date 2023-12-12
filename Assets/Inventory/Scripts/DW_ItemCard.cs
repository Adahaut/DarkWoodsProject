using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DW_ItemCard : MonoBehaviour
{
    [SerializeField] private DW_Item item;

    private TextMeshProUGUI title;
    private TextMeshProUGUI description;

    private Image icon;

    private void Awake()
    {
        title = gameObject.transform.parent.GetChild(2).GetComponent<TextMeshProUGUI>();
        description = gameObject.transform.parent.GetChild(3).GetComponent<TextMeshProUGUI>();
        icon = gameObject.transform.parent.GetChild(4).GetComponent<Image>();
    }
    void Start()
    {
        title.text = item.m_title;
        description.text = item.m_description;
        icon.sprite = item.m_Texture;
    }
    
    public void SetItem(DW_Item new_item) => item = new_item;
    public DW_Item GetItem() => item;
}
