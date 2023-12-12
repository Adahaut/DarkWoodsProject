using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DW_PlayerAttack : MonoBehaviour
{
    public float timeBetweenAttacks;
    private bool can_attack = true;
    [SerializeField] private Collider attack_collider;

    private void Update()
    {
        Attack();
    }

    public void Attack()
    {        
        if(Input.GetKeyDown(KeyCode.Space)) // si le slot d'inventaire utilisé est une arme
        {
            if(can_attack)
            {
                attack_collider.enabled = true;
                can_attack = false;
                StartCoroutine(WaitBeforeNextAttack());
            }
        }
    }

    IEnumerator WaitBeforeNextAttack()
    {
        yield return new WaitForSeconds(0.2f);
        attack_collider.enabled = false;
        //bouton.interactable = false //get le bouton de l'inventaire correspondant et le griser
        yield return new WaitForSeconds(timeBetweenAttacks);
        //bouton.interactable = true
        can_attack = true;
    }
}
