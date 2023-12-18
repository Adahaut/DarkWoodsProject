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
    private DW_Class m_current_class;

    public void OnChangeLeader(DW_Class current_class)
    {
        m_current_class = current_class;
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
        if(m_current_class != null) 
            m_current_class.currentHealth = currentLife;
        // convert in integer;
        if(currentLife <= 0)
        {
            Die();
        }
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.E)) 
        {
            TakeDamage(10);
            Debug.Log("Take Dammage");
        }
    }

    private void Die()
    {
        //player death
        if(gameObject.tag == "Player")
        {
            DW_GM_Classes.Instance.ClassDeath(gameObject.GetComponent<DW_ClassController>().currentClass);
        }

        //enemies death
        if(gameObject.tag == "Enemy")
        {
            Destroy(gameObject);
            //Drop
        }
    }
}
