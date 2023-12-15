using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using static UnityEditor.PlayerSettings;
using static Node_script;
using static DW_A_Star;
using UnityEngine.TextCore.Text;

public class DW_AiMovement : MonoBehaviour
{
    public DW_Character character;
    public DW_Character player;


    [SerializeField] private List<Vector2> Path = new List<Vector2>();


    private void Update()
    {
        Vector2 pos = character.GetPos();
        if (Path.Count > 0)
        {
            Move();
        }

        //if(character.GetPos() != player.GetPos())
        //{
        //    return pathFinder.A(character.GetPos(), player.GetPos(), out Path);
        //}
            
        //return NodeState.FAILURE;
    }

    private void Move()
    {
        Vector2 pos = character.GetPos();
        string dir = GetDirection(pos);


        if (dir == character.Rotation)
        {
            character.StartCharacterMove(.5f);
        }
        else
            Turn(dir);
    }

    private void Turn(string direction)
    {
        // index 0 = Up / 1 = Down/ 2 = Left / 3 = Right
        // int 0 = 0° of rotation for be in the good direction / 1 = 90° / 2 = -90° / 3 = 180° 
        int[,] difference_between_direction =
        {
            {0, 3, 1, 2},
            {3,0,2,1},
            {2, 1, 0, 3 },
            {1,2,3,0 }
        };

        int currentDir = GetDirectionIndex(character.Rotation);
        int targetDir = GetDirectionIndex(direction);

        switch (difference_between_direction[currentDir, targetDir])
        {
            case 1:
                character.StartCharacterTurn(.5f, false, false);
                break;
            case 2:
                character.StartCharacterTurn(.5f, true, false);
                break;
            case 3:
                character.StartCharacterTurn(.5f, false, true);
                break;
        }
    }

    private int GetDirectionIndex(string direction) 
    {
        switch (direction)
        {
            case "Up":
                return 0;

            case "Down":
                return 1;

            case "Left":
                return 2;

            case "Right":
                return 3;
        }
        return -1;
    }
    private string GetDirection(Vector2 positionPlayer)
    {
        while (positionPlayer == Path[0])
        {
            Path.RemoveAt(0);
        }
        if (Path[0].y == positionPlayer.y)
        {
            if(positionPlayer.x < Path[0].x) { return "Left"; }
            if (positionPlayer.x > Path[0].x) { return "Right"; }
        }
        else if (Path[0].x == positionPlayer.x)
        {
            if (positionPlayer.y < Path[0].y) { return "Down"; }
            if (positionPlayer.y > Path[0].y) { return "Up"; }
        }

        return "Null";
    }

    public void SetPath(List<Vector2> path)
    {
        Path = path;
    }
}


