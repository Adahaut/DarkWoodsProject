using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Node_script;

public class DW_ReturnToIniTialPos : Node
{
    private DW_AiMovement movement;
    private DW_Character character;
    private DW_A_Star pathFinder;
    private Vector2 initial_pos;
    private List<Vector2> path;

    public DW_ReturnToIniTialPos(DW_AiMovement _movement, DW_Character _character, Vector2 initialPos)
    {
        movement = _movement;
        character = _character;
        initial_pos = initialPos;
        pathFinder = new DW_A_Star(character);
    }
    public override NodeState Evaluate()
    {
            NodeState state = pathFinder.DijkstraPath(character.GetPos(), initial_pos, out path, true);
            movement.SetPath(path);
            return state;
    }
}
