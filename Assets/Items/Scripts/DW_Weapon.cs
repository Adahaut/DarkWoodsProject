using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DW_Weapon : DW_Item
{

    public int pourcentDamage = 0;
    public override void Use()
    {
        Debug.Log("Weapon");
        // GameObject.FindAnyObjectByType<DW_PlayerAttack>().Attack();
        attack(20);
    }

    public void attack(int pourcent_bonus)
    {
        Debug.Log("attack");
    }
}
