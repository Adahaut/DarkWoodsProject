using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DW_EnnemySpawner : MonoBehaviour
{
    public GameObject ennemy;
    public GameObject player;
    public Vector2Int Position = new();
    // Start is called before the first frame update
    void Start()
    {
        GameObject go = Instantiate<GameObject>(ennemy);
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
