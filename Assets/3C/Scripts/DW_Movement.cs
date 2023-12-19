using System.Collections;
using System.Collections.Generic;
using System.Data.Common;
using System.Diagnostics;
using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using UnityEngine.UIElements.Experimental;

public class Movement : MonoBehaviour
{
    [SerializeField] DW_Character player;
    [SerializeField] DW_search Slider;
    public DW_CampFire camp_fire;
    public DW_Interactions interactions;
    public DW_DropController dropController;

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
    private void Update()
    {
        if (interactions.SearchItem())
        {
            if (Input.GetKey(KeyCode.P) )
            {
                player.GetComponent<DW_Interactions>().InteractCampFire(Slider, dropController);
            }
        }
        if(Input.GetKeyUp(KeyCode.P))
        {
            if (Slider.radialIndicator.fillAmount < 1)
            {
                Slider.ResetSlider();
            }
        }
    }
}

