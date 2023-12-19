using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DW_GridMap : MonoBehaviour
{
    public static DW_GridMap Instance;

    public Vector2Int origine = new(0, 0);

    public GameObject path;
    public GameObject wall;
    public GameObject origin;


    public int[,] Grid = {
                    {1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1 },  // 1 = world border
                    {1,0,0,0,0,2,0,2,0,2,0,2,2,2,0,2,0,2,0,2,0,0,1 },  // 0 = innaccessibility
                    {1,0,0,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,0,1 },  // 5 = character
                    {1,0,2,2,0,2,0,2,0,2,0,2,2,2,0,2,0,2,0,2,2,2,1 },  // 6 = spawn
                    {1,0,2,2,0,0,0,0,0,0,0,2,0,2,0,0,0,0,0,0,2,2,1 },  // 2 = path
                    {1,0,0,2,2,2,2,2,2,0,0,2,2,2,0,0,2,2,2,2,2,0,1 },  // 9 = exit
                    {1,0,2,2,2,0,0,2,2,0,0,0,2,0,0,0,2,2,0,2,2,2,1 },  // 3 = statue de paladin
                    {1,0,0,2,0,0,0,2,2,0,0,2,2,2,0,0,2,2,0,0,2,0,1 },
                    {1,0,0,2,0,0,0,2,2,0,0,2,2,2,0,0,2,2,0,0,2,0,1 },
                    {1,0,2,2,2,0,0,2,2,0,0,2,2,2,0,0,2,2,0,2,2,2,1 },
                    {1,0,0,2,2,2,2,2,2,0,0,0,0,0,0,0,2,2,2,2,2,0,1 },
                    {1,0,2,2,2,0,0,2,2,0,0,0,0,0,0,0,2,2,0,2,2,2,1 },
                    {1,0,0,2,0,0,0,2,2,2,2,2,2,2,2,2,2,2,0,0,2,0,1 },
                    {1,0,0,2,0,0,0,2,2,2,2,2,0,2,2,2,2,2,0,0,2,0,1 },
                    {1,0,2,2,2,0,0,2,2,2,2,2,0,2,2,2,2,2,0,2,2,2,1 },
                    {1,0,0,2,2,2,2,2,2,2,0,0,0,0,0,2,2,2,2,2,2,0,1 },
                    {1,0,2,2,2,0,0,0,0,2,0,0,0,0,0,2,0,0,0,2,2,2,1 },
                    {1,0,0,2,2,0,2,0,2,2,2,0,2,0,2,2,2,0,2,0,2,0,1 },
                    {1,0,0,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,0,1 },
                    {1,0,0,0,2,0,2,0,2,0,2,0,2,0,2,0,2,0,2,0,0,0,1 },
                    {1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1 },
                    {1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1 },
                    {1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1 }};



    private void test()
    {
        GameObject go;
        for (int i = 0; i < 23; i++)
        {
            for (int j = 0; j <23; j++)
            {
                if(i == 0 && j == 0)
                {
                    go = Instantiate<GameObject>(wall);
                }
                else if (Grid[i,j] == 2)
                {
                     go = Instantiate<GameObject>(path);

                }
                else
                {
                    go = Instantiate<GameObject>(wall);

                }

                go.transform.position = new Vector3(-j * 10, 1, -i * 10);


            }
        }
    }

    private void Awake()
    {
        if(Instance == null ) { Instance = this; }
        test();
    }

    public Vector2Int SetMyPosInGrid(int ID, Vector2Int previous_pos, Vector2Int spawnPosInWorld)
    {
        Grid[previous_pos.x, previous_pos.y] = 2;
        Grid[Mathf.Abs(spawnPosInWorld.x / 10), Mathf.Abs(spawnPosInWorld.y / 10)] = ID;
        return new Vector2Int(Mathf.Abs(spawnPosInWorld.x / 10), Mathf.Abs(spawnPosInWorld.y / 10));
    }

    public void Spawn(int ID, Vector2Int spawnPosInWorld)
    {
        //if (Grid[Mathf.Abs(spawnPosInWorld.x/10), Mathf.Abs(spawnPosInWorld.y/10)] == 2) 
        {
            Grid[Mathf.Abs(spawnPosInWorld.x/10), Mathf.Abs(spawnPosInWorld.y/10)] = ID;
        }
        //else 
        //{
        //    Debug.LogError("Two Characters are on the same position spawn");
        //}
    }

    public Vector2Int GetPlayerPosOnGrid()
    {
        for (int i = 0; i < Grid.Length; i++)
        {
            for (int j = 0; j < Grid.Length; j++)
            {
                if (Grid[i, j] == 5)
                {
                    return new Vector2Int(j, i);

                }
            }
        }
        return Vector2Int.zero;

    }

}
