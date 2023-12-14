using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using static UnityEditor.FilePathAttribute;
using UnityEngine.TextCore.Text;
using Palmmedia.ReportGenerator.Core.CodeAnalysis;

public class DW_Interactions : MonoBehaviour
{
    public float timeBetweenAttacks;
    private bool can_attack = true;
    [SerializeField] private Collider attack_collider;

    [SerializeField] DW_ClassHolderRef class_holder;
    private DW_Character player_character;

    Vector2 checkArroundPlayer;

    private void Awake()
    {
        player_character = this.GetComponent<DW_Character>();
    }
    private void Update()
    {
    }

    public void Attack(DW_Weapon weapon)
    {
        if(can_attack)
        {
            float damage = Random.Range(class_holder.classRef.minattackDamage, class_holder.classRef.maxattackDamage);
            float finalDamage = damage * ((class_holder.classRef.currentPercentDamage + weapon.pourcentDamage) / 100);


            if (CheckForwardPLayer(player_character.Rotation, 3) == true)
            {
                Debug.Log("Enemy forward");
                RaycastHit hit;
                
                if (Physics.Raycast(this.transform.position + new Vector3(0, 0.5f,0), this.transform.forward, out hit,10))
                {
                    Debug.Log("Damage : " + finalDamage);
                    Debug.Log(hit.collider.name);
                    if (hit.collider.tag == "Enemy")
                    {
                        hit.collider.GetComponent<DW_LifeManager>().TakeDamage(finalDamage);
                        can_attack = false;
                        StartCoroutine(WaitBeforeNextAttack());
                    }
                }
            }
            
        }
    }

    public void ConsumeItem(DW_consumable consumable, specialSourceType sourceneeded)
    {
        if (class_holder.classRef.specialSourceType == sourceneeded)
        {
            if (class_holder.classRef.currentHealth < class_holder.classRef.maxHealth
            || class_holder.classRef.specialSourceAmount < 100)
            {
                class_holder.classRef.currentHealth += consumable.healthRestoreAmount;
                class_holder.classRef.specialSourceAmount += consumable.specialRestoreAmount;
                class_holder.UpdateHealthBar();
                class_holder.UpdateSpecialBar();
                Debug.Log("consumed");
            }
        }
        
    }

    public void Interact(DW_interractible interractible)
    {
        if(interractible.m_Item == Item.Key)
        {
            if (CheckForwardPLayer(player_character.Rotation, 4) == true )
            {
                RaycastHit hit;
                if(Physics.Raycast(this.transform.position + new Vector3 (0,0.5f,2),this.transform.forward,out hit,10))
                {
                    if(hit.collider.tag == "Door")
                    {
                        Destroy(hit.transform.gameObject);
                        Debug.Log("door opened");
                    }
                }
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

    private bool CheckForwardPLayer(string rotation, int value_needed)
    {

        int[,] _grid = DW_GridMap.Instance.Grid;

        switch (rotation)
        {
                // changer la case devant si l'ia est mort sinon on peut taper que 1 fois.
            case "Left":
                if (_grid[player_character.CharacterY, player_character.CharacterX - 1] == value_needed)
                {
                    DW_GridMap.Instance.SetMyPosInGrid(2, new Vector2Int(player_character.CharacterY, player_character.CharacterX - 1), new Vector2Int(player_character.CharacterY, player_character.CharacterX - 1));

                    return true;
                }
                else
                {
                    return false;
                }
            case "Right":
                if (_grid[player_character.CharacterY, player_character.CharacterX + 1] == value_needed)
                {
                    DW_GridMap.Instance.SetMyPosInGrid(2, new Vector2Int(player_character.CharacterY, player_character.CharacterX + 1), new Vector2Int(player_character.CharacterY, player_character.CharacterX + 1));
                    return true;
                }
                else
                {
                    return false;
                }
            case "Up":
                if (_grid[player_character.CharacterY - 1, player_character.CharacterX] == value_needed)
                {
                    DW_GridMap.Instance.SetMyPosInGrid(2, new Vector2Int(player_character.CharacterY - 1, player_character.CharacterX), new Vector2Int(player_character.CharacterY - 1, player_character.CharacterX));
                    return true;
                }
                else
                {
                    return false;
                }
            case "Down":
                if (_grid[player_character.CharacterY + 1, player_character.CharacterX] == value_needed)
                {
                    DW_GridMap.Instance.SetMyPosInGrid(2, new Vector2Int(player_character.CharacterY + 1, player_character.CharacterX), new Vector2Int(player_character.CharacterY + 1, player_character.CharacterX));
                    return true;
                }
                else
                {
                    return false;
                }
            default:
                break;
        }
        return false;
    }




}
