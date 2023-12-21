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
    [SerializeField] private DW_ClassHolderRef class_holder_ref;
    private Animator animator;


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
        if(gameObject.tag == "Enemy")
        {
            animator = GetComponent<Animator>();
            if(animator == null)
            {
                animator = GetComponentInChildren<Animator>();
            }
        }
    }

    public void TakeDamage(float damage)
    {
        currentLife -= damage;

        if (this.tag == "Player")
        {
            foreach (DW_Class c in this.GetComponent<DW_ClassController>().classes)
            {
                if (c.shouldBeAggro)
                {
                    c.currentHealth -= damage;
                    class_holder_ref.UpdateHealthBar();

                    if (c.currentHealth <= 0)
                        Die(c);
                }
            }
        }
        else if(this.tag == "Enemy")
        {
            animator.SetBool("TakeDamage", true);
        }

        // convert in integer;
        if(currentLife <= 0)
        {
            Die();
        }
    }


    private void Die(DW_Class class_died = null)
    {
        //player death
        if(gameObject.tag == "Player")
        {
            DW_GM_Classes.Instance.ClassDeath(class_died);
            Vector2 Pos = gameObject.GetComponent<DW_Character>().GetPos();
            DW_GridMap.Instance.Grid[(int)Pos.x, (int)Pos.y] = 2;
            this.GetComponent<DW_ClassController>().ResetAggro(); 
        }

        //enemies death
        if(gameObject.tag == "Enemy")
        {
            Destroy(gameObject);
            //Drop
        }
    }
}
