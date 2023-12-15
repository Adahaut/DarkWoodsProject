using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DW_Weapon : DW_Item
{
    DW_Class Class;
    public int pourcentDamage = 0;
    public override void Use()
    {
        Debug.Log("Weapon");
        GameObject.FindAnyObjectByType<DW_Interactions>().Attack(this);
    }
}
