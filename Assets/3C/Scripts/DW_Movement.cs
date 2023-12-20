using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.HID;
using UnityEngine.UI;

public class Movement : MonoBehaviour
{
    [SerializeField] DW_Character player;
    [SerializeField] private DW_MenuController menus;

    public float walkSpeed = 1.0f;
    public float turnSpeed = 1.5f;
    public void OnMovePlayer(InputAction.CallbackContext ctx)
    {
        if (ctx.started)
        {
            player.StartCharacterMove(walkSpeed);
        }
    }
    public void OnTurnCameraRight(InputAction.CallbackContext ctx)
    {
        if (ctx.started)
        {
            Vector2 vector2 = ctx.ReadValue<Vector2>();
            if (vector2.x < 0)
            {
                player.StartCharacterTurn(turnSpeed, false, false);
            }
            else if (vector2.x > 0)
            {
                player.StartCharacterTurn(turnSpeed, true, false);
            }
            else if (vector2.y < 0)
            {
                player.StartCharacterTurn(turnSpeed * 2, true, true);
            }
        }
    }
    private float timer_press = 0;
    public float speed_timer = 1;
    public Image fill_search;
    public GameObject inspection_ui;

    private DW_CampSearch camp_fire;

    public void Update()
    {
        if(Input.GetKey(KeyCode.P))
        {

            RaycastHit hit;
            if (Physics.Raycast(player.transform.position + new Vector3(0, 0.5f, 0), player.transform.forward, out hit, 15))
            {
                if (hit.collider.tag == "test" && !hit.collider.gameObject.GetComponent<DW_CampSearch>().hasBeenSearched)
                {
                    inspection_ui.SetActive(true);
                    timer_press += Time.deltaTime * speed_timer;
                    fill_search.fillAmount = timer_press / 3;

                    camp_fire = hit.collider.gameObject.GetComponent<DW_CampSearch>();
                }
                else
                {
                    inspection_ui.SetActive(true);
                    fill_search.fillAmount = 1;
                    fill_search.color = Color.red;
                }
            }
        }

        

        if(Input.GetKeyUp(KeyCode.P))
        {
            if(timer_press < 3)
            {
                timer_press = 0;
            }
            else
            {
                if (timer_press >= 3)
                {
                    DW_Item item = camp_fire.SearchCamp();

                    if (item != null)
                        GameObject.FindObjectOfType<DW_DropController>().Drop(item.m_Item, player.transform.position);

                    timer_press = 0;
                }
                else
                {
                    timer_press += Time.deltaTime;
                }
            }

            inspection_ui.SetActive(false);
            fill_search.fillAmount = 0;
            fill_search.color = Color.white;
        }
    }

    public void PauseActive(InputAction.CallbackContext ctx)
    {
        if(ctx.started)
        {
            menus.pauseIsActive = true;
            if(menus.pauseIsActive)
            {
                menus.Pause();
            }
        }
    }



}

