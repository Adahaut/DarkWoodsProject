using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.PlasticSCM.Editor.WebApi;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;
using UnityEngine.UI;
using static UnityEngine.UI.Button;

public class DW_NavigationInventory : MonoBehaviour
{
    public List<GameObject> inventorySlot = new();

    [SerializeField]
    private Button current_button = null;

    [SerializeField] private int current_slot_selected = 0;
    void Start()
    {
        current_slot_selected = 0;
        GetProperties();
    }


    void Update()
    {
        //if(Input.GetKeyDown(KeyCode.E)) 
        //{
        //    current_button.image.color = current_button.colors.normalColor;
        //    current_slot_selected++;
        //    current_slot_selected = current_slot_selected >= inventorySlot.Count ? 0 : current_slot_selected;
        //    GetProperties();
        //    current_button.image.color = current_button.colors.selectedColor;

        //}
        //if(Input.GetKeyDown(KeyCode.A))
        //{
        //    current_button.Press();
        //}
    }



    private void GetProperties()
    {
        current_button = inventorySlot[current_slot_selected].GetComponent<Button>();
    }
}
