using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DW_LifeManager : MonoBehaviour
{
    [SerializeField] private int max_life;
    public int currentLife;
    public int damage;

    private void Start()
    {
        currentLife = max_life;    
    }

    public void TakeDamage(int damage)
    {
        currentLife -= damage;

        if(currentLife <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        //player death
        if(gameObject.tag == "Player")
        {
            Debug.Log("mort du joueur");
        }

        //enemies death
        if(gameObject.tag == "Enemy")
        {
            Destroy(gameObject);
            //Drop
        }
    }
    
}
