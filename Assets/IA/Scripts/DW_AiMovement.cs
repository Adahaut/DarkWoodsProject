using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class DW_AiMovement : MonoBehaviour
{
    public DW_Character character;
    public DW_Character player;

    // Update is called once per frame
    void Update()
    {
        Vector2 pos = character.GetPos();
        if (Path.Count > 0)
        {
            Move();
        }

        if(character.GetPos() != player.GetPos())
            A(character.GetPos(), player.GetPos());
       
        if (Input.GetKeyDown(KeyCode.E))
        {
            A(_start, _target);
        }
    }

    private void Move()
    {
        Vector2 pos = character.GetPos();
        string dir = GetDirection(pos);


        if (dir == character.Rotation)
        {
            Debug.Log(" ai mov");
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
        if (Path.Count > 0)
        {
            if (positionPlayer == Path[0])
            {
                Path.RemoveAt(0);
            }
            if (Path[0].y == positionPlayer.y)
            {
                if (positionPlayer.x > Path[0].x) { return "Left"; }
                if (positionPlayer.x < Path[0].x) { return "Right"; }
            }
            else if (Path[0].x == positionPlayer.x)
            {
                if (positionPlayer.y < Path[0].y) { return "Down"; }
                if (positionPlayer.y > Path[0].y) { return "Up"; }
            }
            
        
        }
        return character.Rotation;
    }


    [SerializeField] private Vector2 _target;
    [SerializeField] private Vector2 _start;
    private Vector2 current_position;
    private Vector2 real_current_position;

    private static Vector2 null_vector = new Vector2(200,200);

    [SerializeField]private List<Vector2> Path = new List<Vector2>();
    [SerializeField] private List<Vector2> SavePasedPoint = new List<Vector2>();
    public List<Vector2> A(Vector2 start, Vector2 target)
    {
        _target = target;
        current_position = start;

        SavePasedPoint.Clear();
        Path.Clear();

        while (current_position != _target) // while no way found
        {
            real_current_position = current_position;

            if (!Path.Contains(current_position))
                Path.Add(current_position);

            current_position = FindNearest(current_position);

            if (current_position == null_vector)
            {
                Path.Remove(real_current_position);

                if (Path.Count == 0)
                {
                    return null;
                }

                current_position = Path[Path.Count - 1];


            }
            if (Path.Count > 1000 || SavePasedPoint.Count > 1000)
            {
                SavePasedPoint.Clear();
                Path.Clear();
                return null;
            }

        }

        //if (current_position == _target)
            //Path.Add(current_position);

        return Path;

    }

    private Vector2 FindNearest(Vector2 testValue)
    {
        List<float> heuristiqueDistance = new List<float>(); // The distance between the two object + the distance between the object we want to go and the target
        List<Vector2> neighbors = character.GetPathAround(testValue);/*Get all the neighbors the ennemie can go on*/

        if (neighbors.Count == 0)
        {
            SavePasedPoint.Add(testValue);
            return null_vector;
        }

        int indexNearest = -1;

        for (int i = 0; i < neighbors.Count; i++)
        {
            heuristiqueDistance.Add(Vector2.Distance(neighbors[i], testValue) + Vector3.Distance(neighbors[i], _target));

            if ((indexNearest == -1 ? Mathf.Infinity : heuristiqueDistance[indexNearest]) >= heuristiqueDistance[i] && !SavePasedPoint.Contains(neighbors[i]) && !Path.Contains(neighbors[i])) // is newDistance < nearest distance calculate  
            {
                indexNearest = i;
            }
        }

        if (indexNearest == -1) // no distance find
        {
            SavePasedPoint.Add(testValue);
            return null_vector;
        }

        if (neighbors[indexNearest] == null)
            SavePasedPoint.Add(testValue);

        return neighbors[indexNearest];
    }




}


