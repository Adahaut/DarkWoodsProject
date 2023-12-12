using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DW_CheckCollisionInAttack : MonoBehaviour
{
    [SerializeField] private DW_LifeManager life_manager;

    private void Start()
    {
        //for enemies
        life_manager = GetComponent<DW_LifeManager>();
        
        //for player
        if(life_manager == null)
        {
            life_manager = GetComponentInParent<DW_LifeManager>();
        }
        
    }

    private void OnTriggerEnter(Collider other)
    {
        //for Player
        if(transform.parent.gameObject.tag == "Player")
        {
            if (other.gameObject.tag == "Enemy")
            {
                other.gameObject.GetComponent<DW_LifeManager>().TakeDamage(life_manager.damage);
            }
        }

        //for enemies
        if(transform.parent.gameObject.tag == "Enemy")
        {
            if(other.gameObject.tag == "Player")
            {
                other.gameObject.GetComponent<DW_LifeManager>().TakeDamage(life_manager.damage);
            }
        }

    }
}
