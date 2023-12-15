using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Node_script;
using UnityEngine.TextCore.Text;

public class DW_A_Star
{
    [SerializeField] private Vector2 _target;
    [SerializeField] private Vector2 _start;
    private DW_Character character;
    private Vector2 current_position;
    private Vector2 real_current_position;

    private static Vector2 null_vector = new Vector2(200, 200);


    [SerializeField] private List<Vector2> SavePasedPoint = new List<Vector2>();


    public DW_A_Star(DW_Character _character)
    {
        character = _character;
    }

    public NodeState A(Vector2 start, Vector2 target, out List<Vector2> Path, bool OnTarget)
    {
        Path = new List<Vector2>();
        _target = target;
        current_position = start;

        SavePasedPoint.Clear();
        Path.Clear();

        while (current_position != _target) // while no way found
        {
            real_current_position = current_position;

            if (!Path.Contains(current_position))
                Path.Add(current_position);

            current_position = FindNearest(current_position, Path);

            if (current_position == null_vector)
            {
                Path.Remove(real_current_position);

                if (Path.Count == 0)
                {
                    return NodeState.FAILURE;
                }

                current_position = Path[Path.Count - 1];


            }
            if (Path.Count > 1000 || SavePasedPoint.Count > 1000)
            {
                SavePasedPoint.Clear();
                Path.Clear();
                return NodeState.FAILURE;
            }

        }

        if (current_position == _target && OnTarget)
            Path.Add(current_position);

        return NodeState.SUCCESS;

    }

    private Vector2 FindNearest(Vector2 testValue, List<Vector2> Path)
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
