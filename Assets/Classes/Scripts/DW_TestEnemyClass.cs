using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

// THIS CLASS IS ONLY FOR TESTS

public class DW_TestEnemyClass : MonoBehaviour
{
    public bool reduceFov = false;
    private float cooldownRestauration = 10.0f;
    // Update is called once per frame
    void Update()
    {
        if(!reduceFov)
        {
            return;
        }
        if (cooldownRestauration <= 0)
        {
            this.GetComponent<SphereCollider>().radius *= 2;
            cooldownRestauration = 10;
            reduceFov = false;
            return;
        }

        cooldownRestauration -= Time.deltaTime;
    }

    public void ReduceFov()
    {
        this.GetComponent<SphereCollider>().radius /= 2;
        reduceFov = true;
    }

    public void ReceiveDamage(DW_Class damagerClass)
    {
        // this health -= class rdm dmg
        // if dmg class == paladin -> Faith -= 20% max faith
    }
}
