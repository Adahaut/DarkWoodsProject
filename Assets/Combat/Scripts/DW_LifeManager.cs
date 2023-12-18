using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class DW_LifeManager : MonoBehaviour
{
    [SerializeField] private float max_life;
    public float currentLife;
    public int damage;
    private DW_Character player_character;

    public void OnChangeLeader(DW_Class current_class)
    {
        currentLife = current_class.currentHealth;
        max_life = current_class.maxHealth;

        float dmg = Random.Range(current_class.minattackDamage, current_class.maxattackDamage);
        float percentDmg = Random.Range(current_class.minPercentDamage, current_class.maxPercentDamage);
        damage = Mathf.RoundToInt(percentDmg * dmg);
    }
    private void Start()
    {
        currentLife = max_life;    
    }

    public void TakeDamage(float damage)
    {
        //Debug.Log(damage);
        currentLife -= damage;
        // convert in integer;
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
