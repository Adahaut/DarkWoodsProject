using System.Collections.Generic;
using UnityEngine;
using static Node_script;

public class DW_A_Star
{
    [SerializeField] private List<Vector2> SavePasedPoint = new List<Vector2>();
    [SerializeField] private Queue<Vector2> queue = new Queue<Vector2>();
    Dictionary<char, int> _pathDictionnary = new Dictionary<char, int>();
    Dictionary<char, List<char>> _result = new();
    private DW_Character character;
    private Vector2 current_position;
    List<Vector2> _sommets = new List<Vector2>();

    public DW_A_Star(DW_Character _character)
    {
        character = _character;
        SetSommet();
    }

    private void SetSommet()
    {
        for (int i = 0; i < 20; i++)
        {
            for (int j = 0; j < 20; j++)
            {
                _sommets.Add(new Vector2(i, j));
            }
        }
    }

    public NodeState DijkstraPath(Vector2 sommet, Vector2 target, out List<Vector2> Path, bool OnTarget)
    {
        Dictionary<Vector2, int> _pathDictionnary = new Dictionary<Vector2, int>();
        Dictionary<Vector2, List<Vector2>> _result = new();
        Queue<Vector2> list = new Queue<Vector2>();
        Vector2 currentSommet = Vector2.zero;

        for (int i = 0; i < _sommets.Count; i++)
        {
            _pathDictionnary[_sommets[i]] = int.MaxValue;
            _result[_sommets[i]] = new();
            if (sommet == _sommets[i])
            {
                _pathDictionnary[_sommets[i]] = 0;
                _result[_sommets[i]] = new() { _sommets[i] };
            }
        }
        list.Enqueue(sommet);
        while (list.Count > 0)
        {
            currentSommet = list.Dequeue();
            List<Vector2> voisin = character.GetPathAround(currentSommet);
            for (int i = 0; i < voisin.Count; i++)
            {
                if ((_pathDictionnary[currentSommet] + 1) < _pathDictionnary[voisin[i]])
                {
                    _pathDictionnary[voisin[i]] = _pathDictionnary[currentSommet] + 1;
                    AddList(_result[voisin[i]], _result[currentSommet]);
                    _result[voisin[i]].Add(voisin[i]);
                    list.Enqueue(voisin[i]);
                }
            }

        }
        Path = _result[target];
        return NodeState.SUCCESS;

    }

    public void AddList(List<Vector2> take, List<Vector2> add, int index = 0)
    {
        for (int i = index; i < add.Count; i++)
        {
            take.Add(add[i]);
        }
    }

    /// <summary>
    /// ////////////////////
    /// </summary>
    /*[SerializeField] private Vector2 _target;
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
        Queue<Vector2> queue = new();
        Path = new List<Vector2>();
        _target = target;
        queue.Enqueue(start);

        SavePasedPoint.Clear();
        Path.Clear();

        while (queue.Count > 0) // while no way found
        {
            current_position = queue.Dequeue();

            if (!Path.Contains(current_position))
                Path.Add(current_position);

            FindNearest(current_position, Path);

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
        List<Vector2> neighbors = character.GetPathAround(testValue);//Get all the neighbors the ennemie can go on

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

        return  neighbors[indexNearest];
    }*/
}
