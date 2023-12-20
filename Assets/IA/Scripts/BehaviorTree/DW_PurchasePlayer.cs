using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using static Node_script;
public class DW_PurchasePlayer : Node
{
    private DW_AiMovement movement;
    private DW_Character character;
    private DW_A_Star pathFinder;
    private DW_Character player;
    private List<Vector2> path;

    public DW_PurchasePlayer(DW_AiMovement _movement, DW_Character _character, DW_Character _player)
    {
        movement = _movement;
        character = _character;
        player = _player;
        pathFinder = new DW_A_Star(character);
    }
    public override NodeState Evaluate()
    {
        if (movement.IsPathNull())
        {
            NodeState state = pathFinder.DijkstraPath(character.GetPos(), player.GetPos(), out path, false);
            movement.SetPath(path);
            return state;
        }
        else
            return NodeState.SUCCESS;

    }
}
