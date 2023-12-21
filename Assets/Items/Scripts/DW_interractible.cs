using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class DW_interractible : DW_Item
{
    public override bool Use()
    {

       return GameObject.FindAnyObjectByType<DW_Interactions>().Interact(this);
        
        // in player attack call interractible function. 
    }
} 