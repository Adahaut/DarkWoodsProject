using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Node_script;
using UnityEngine.TextCore.Text;
using System.Net;
using System;

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
        for (int i = 0; i < 23; i++)
        {
            for (int j = 0; j < 23; j++)
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
            if (currentSommet == target)
            {
                break;
            }
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
}
