using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.TextCore.Text;
using JetBrains.Annotations;
using UnityEngine.UI;

public class DW_Interactions : MonoBehaviour
{
    public float timeBetweenAttacks;
    private bool can_attack = true;
    [SerializeField] private Collider attack_collider;

    [SerializeField] DW_ClassHolderRef class_holder;
    [SerializeField] DW_LifeManager life_manager;
    [SerializeField] private DW_Character player_character;
    DW_CampFire camp_fire;

    private void OnEnable()
    {
    }

    public void Attack(DW_Weapon weapon)
    {
        if(can_attack)
        {
            //calculating damage according to class

            float damage = Random.Range(class_holder.classRef.minattackDamage, class_holder.classRef.maxattackDamage);
            float finalDamage = damage * ((class_holder.classRef.currentPercentDamage + weapon.pourcentDamage) / 100);

            if (CheckForwardPLayer(player_character.Rotation, 3 ) == true|| CheckForwardPLayer(player_character.Rotation,2) == true)
            {
                RaycastHit hit;
                if (Physics.Raycast(this.transform.position + new Vector3(0, 0.5f,0), this.transform.forward, out hit,15))
                {
                    if (hit.collider.tag == "Enemy")
                    {
                        Debug.Log("degat reçut : " + finalDamage);
                        hit.collider.GetComponent<DW_LifeManager>().TakeDamage(finalDamage);
                        can_attack = false;
                        StartCoroutine(WaitBeforeNextAttack());
                        if(hit.collider.GetComponent<DW_LifeManager>().currentLife <= 0)
                        {
                            Debug.Log("enemy dead");
                        }
                    }
                }
            }
            
        }
    }

    public void InteractCampFire(DW_search slider , DW_DropController dropController) 
    {
        DW_CampFire camp_fire= null;
        RaycastHit hit;
        UnityEngine.Debug.Log("aaaa");
        if (Physics.Raycast(this.transform.position + new Vector3(0, 0.5f, 0), this.transform.forward, out hit, 100))
        {
            Debug.Log("tyuiop");
            if (hit.collider.tag == "CampFire")
            {
                if (slider.radialIndicator.fillAmount < 1)
                {
                    slider.Search();
                }
                else
                {
                    camp_fire = hit.collider.gameObject.GetComponent<DW_CampFire>();
                    dropController.Drop(camp_fire.RandomItem().m_Item, this.transform.position);
                    slider.ResetSlider();
                    camp_fire.CampFire();
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

    public bool Interact(DW_interractible interractible)
    {
        if (CheckForwardPLayer(player_character.Rotation, 4)) 
        {   
            if (interractible.m_Item == Item.Key)
            {
                RaycastHit hit;
                if (Physics.Raycast(this.transform.position + new Vector3(0, 0.5f, 0), this.transform.forward, out hit, 10))
                {
                    Debug.Log(hit.collider.tag);
                    if (hit.collider.tag == "Door")
                    {
                        Debug.Log("disappeared");
                        StartCoroutine(AnimDoor(1, hit.collider.gameObject));
                        return true;
                    }
                }
            }
        }
        return false;
    }

    public bool SearchItem()
    {
        if(CheckForwardPLayer(player_character.Rotation, 7))
        {
            return true;
        }
        return false;
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

    public bool CheckForwardPLayer(string rotation, int value_needed)
    {
        int[,] _grid = DW_GridMap.Instance.Grid;
        switch (rotation)
        {
            case "Left":
                if (_grid[player_character.CharaY, player_character.CharaX - 1] == value_needed)
                {
                    if (value_needed != 7)
                    {
                        DW_GridMap.Instance.SetMyPosInGrid(2, new Vector2Int(player_character.CharaY, player_character.CharaX - 1), new Vector2Int(player_character.CharaY, player_character.CharaX - 1));
                    }
                    return true;
                }
                else
                {
                    return false;
                }
            case "Right":
                if (_grid[player_character.CharaY, player_character.CharaX + 1] == value_needed)
                {
                    if (value_needed != 7)
                    {
                        DW_GridMap.Instance.SetMyPosInGrid(2, new Vector2Int(player_character.CharaY, player_character.CharaX + 1), new Vector2Int(player_character.CharaY, player_character.CharaX + 1));
                    }
                    return true;
                }
                else
                {
                    return false;
                }
            case "Up":
                if (_grid[player_character.CharaY - 1, player_character.CharaX] == value_needed)
                {
                    if (value_needed != 7)
                    {
                        DW_GridMap.Instance.SetMyPosInGrid(2, new Vector2Int(player_character.CharaY - 1, player_character.CharaX), new Vector2Int(player_character.CharaY - 1, player_character.CharaX));
                    }
                    return true;
                }
                else
                {
                    return false;
                }
            case "Down":
                if (_grid[player_character.CharaY + 1, player_character.CharaX] == value_needed)
                {
                    if (value_needed != 7)
                    {
                        DW_GridMap.Instance.SetMyPosInGrid(2, new Vector2Int(player_character.CharaY + 1, player_character.CharaX), new Vector2Int(player_character.CharaY + 1, player_character.CharaX));
                    }
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

    private IEnumerator AnimDoor(float total_time, GameObject door)
    {
        float time = 0f;
        float start_pos = this.transform.position.y;
        float end_pos = this.transform.position.y + 10f;
        float rotation = 0f;

        while (time /total_time < 1)
        {
            time += Time.deltaTime;
            rotation = Mathf.Lerp(start_pos, end_pos, time /total_time);
            door.transform.position  = new Vector3(door.transform.position.x, rotation, door.transform.position.z);
            yield return null;
        }
        yield return new WaitForSeconds(1);
    }
        
}
