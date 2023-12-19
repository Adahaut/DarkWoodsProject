using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DW_Weapons : MonoBehaviour
{
    DW_Interactions player_attack;
    DW_LifeManager life_manager;
    [SerializeField] private float weapon_time_between_attacks;
    [SerializeField] private int weapon_damage;
    public bool weaponEquipped;

    private void Start()
    {
        player_attack = GameObject.Find("Player").GetComponent<DW_Interactions>();
        life_manager = GameObject.Find("Player").GetComponent<DW_LifeManager>();
    }

    private void Update()
    {
        if (weaponEquipped)
        {
            OnWeaponEquipped();
        }
        else
        {
            OnWeaponUnequipped();
        }

    }

    public void OnWeaponEquipped()
    {
        gameObject.SetActive(false);
        gameObject.SetActive(true);
        player_attack.timeBetweenAttacks = weapon_time_between_attacks;
        life_manager.damage = weapon_damage;
    }

    private void OnWeaponUnequipped()
    {
        player_attack.timeBetweenAttacks = 0.5f;
        life_manager.damage = 1;
    }

}
