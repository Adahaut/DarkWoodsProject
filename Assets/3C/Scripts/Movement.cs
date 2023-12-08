using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.InputSystem;

public class Movement : MonoBehaviour
{
    [SerializeField] Player player;


    public void OnMovePlayer(InputAction.CallbackContext ctx)
    {
        if (ctx.started)
        {
            player.StartPlayerMove(0.5f);
        }
    }
    public void OnTurnCameraRight(InputAction.CallbackContext ctx)
    {
        if (ctx.started)
        {   
            Vector2 vector2 = ctx.ReadValue<Vector2>();
            if(vector2.x < 0)
            {
                player.StartPlayerTurn(1.5f, false);
            }
            else if(vector2.x > 0)
            {
                player.StartPlayerTurn(1.5f, true);
            }
            else if (vector2.y < 0)
            {
                player.StartPlayerTurnBehind(3f);
            }           
        }      
    }
}
