using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DW_Weapon : DW_Item
{
    public int pourcentDamage = 0;
    public override bool Use()
    {
        Debug.Log("Weapon");
        GameObject.FindAnyObjectByType<DW_Interactions>().Attack(this);
        return true;
    }
}
