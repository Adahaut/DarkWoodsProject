using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using static UnityEngine.UI.Image;

public class DW_GridMap : MonoBehaviour
{
    public static DW_GridMap Instance;

    public Vector2Int origine = new(0, 0);

    public GameObject c;
    public GameObject e;

    public int[,] Grid = {
              // 0 1 2 3 4 5 6 7 8 9 0 1 2 3 4 5 6 7 8 9 0 1 2
                {1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,2,1,1,1,1,1,1,1 },  //0     // 1 = world border
                {1,1,1,1,1,1,1,1,1,1,1,1,1,1,2,4,2,1,1,1,1,1,1 },  //1    // 0 = innaccessibility
                {1,1,1,1,1,1,1,1,2,2,2,1,1,1,2,2,2,1,1,1,1,1,1 },  //2     // 2 = character
                {1,1,1,1,1,2,2,4,2,2,2,2,2,2,2,2,2,2,2,2,1,1,1 },  //3     // 6 = spawn
                {1,1,1,2,2,2,1,1,1,1,1,1,1,1,1,2,1,1,1,2,2,2,1 },  //4     // 2 = path
                {1,1,1,1,1,2,2,2,1,1,1,1,1,2,2,2,1,2,2,2,1,1,1 },  //5     // 2 = exit
                {1,1,2,2,2,2,1,1,1,1,1,1,1,2,1,1,1,1,1,2,2,2,1 },  //6     // 3 = statue de paladin
                {1,1,2,1,2,1,1,1,1,1,1,1,1,2,1,1,1,2,2,2,1,1,1 },  //7
                {1,1,2,1,2,1,1,1,1,1,1,1,1,2,1,1,1,1,1,2,2,2,1 },  //8
                {1,1,2,1,1,1,1,1,1,1,1,1,2,2,1,1,1,2,2,2,1,1,1 },  //9
                {1,1,2,1,1,1,1,1,1,1,1,1,2,1,2,2,2,1,1,2,2,2,1 },  //0
                {1,1,2,1,2,1,1,1,1,1,1,2,2,2,2,6,2,2,2,2,1,1,1 },  //1
                {1,1,2,1,2,1,1,1,1,1,1,4,1,1,2,2,2,1,1,2,2,2,1 },  //2
                {1,1,2,2,2,2,1,2,1,1,1,2,1,1,1,4,1,2,2,2,1,1,1 },  //3
                {1,1,2,1,1,2,1,2,1,1,1,2,1,1,1,2,1,1,1,2,2,2,1 },  //4
                {1,1,2,1,1,2,2,2,2,2,2,2,1,1,2,2,2,2,1,2,1,1,1 },  //5
                {1,1,2,2,2,1,1,2,1,2,1,2,1,1,2,2,2,2,1,4,1,1,1 },  //6
                {1,1,2,1,1,1,1,2,1,2,1,2,1,1,2,1,1,2,1,2,1,1,1 },  //7
                {1,1,2,2,2,1,1,1,1,1,1,2,1,1,2,1,1,2,1,2,1,1,1 },  //8
                {1,1,2,1,1,1,1,1,1,1,2,2,2,1,2,1,1,2,2,2,1,1,1 },  //9
                {1,1,2,2,2,2,2,2,2,2,2,2,2,2,2,1,1,1,1,1,1,1,1 },  //0
                {1,1,1,1,1,2,1,1,1,1,2,2,2,1,1,1,1,1,1,1,1,1,1 },  //1
                {1,1,1,1,1,1,1,1,1,1,1,2,1,1,1,1,1,1,1,1,1,1,1 }}; //2

    private void Awake()
    {
        if(Instance == null ) { Instance = this; }
      //  test();
    }

    public Vector2Int SetMyPosInGrid(int ID, Vector2Int previous_pos, Vector2Int spawnPosInWorld)
    {
        Grid[previous_pos.x, previous_pos.y] = 2;
        Grid[Mathf.Abs(spawnPosInWorld.x / 10), Mathf.Abs(spawnPosInWorld.y / 10)] = ID;
        return new Vector2Int(Mathf.Abs(spawnPosInWorld.x / 10), Mathf.Abs(spawnPosInWorld.y / 10));
    }


   /* private void test()
    {
        GameObject go;
        for (int i = 0; i < 23; i++)
        {
            for (int j = 0; j < 23; j++)
            {
                if (Grid[i, j] == 0)
                {
                    go = Instantiate<GameObject>(c, this.transform.parent);
                }
                else if (Grid[i, j] == 1)
                {
                    go = Instantiate<GameObject>(c, this.transform.parent);

                }
                else
                {
                    go = Instantiate<GameObject>(e, this.transform.parent);

                }

                go.transform.position = new Vector3(-i * 10, 10, -j * 10);


            }
        }
    }
   */
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
                if (Grid[i, j] == 2)
                {
                    return new Vector2Int(j, i);

                }
            }
        }
        return Vector2Int.zero;

    }

}
