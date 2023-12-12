using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DW_Weapon : DW_Item
{
    public override void Use()
    {
        Debug.Log("Weapon");
        GameObject.FindAnyObjectByType<DW_PlayerAttack>().Attack();
    }
}
