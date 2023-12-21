using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Node_script;

public class DW_VerifPosition : Node_script.Node
{
    private Vector2 m_Position;
    private DW_Character character;
    private Animator anim;

    public DW_VerifPosition(Vector2 Pos, DW_Character _character, Animator animator) 
    {
        m_Position = Pos;
        character = _character;
        anim = animator;
    }

    public override NodeState Evaluate()
    {

        if (character.GetPos() == m_Position) //if is on initial pos
        {
            anim.SetBool("IsWalking", false);
            return NodeState.SUCCESS;
        }            
        else
            return NodeState.FAILURE;

    }
}
