using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEditor;
using UnityEngine;
using UnityEngine.TextCore.Text;
using UnityEngine.UIElements;

public class DW_Character : MonoBehaviour
{
    public int ID = 0;

    public float waitCooldown = 0.5f;
    private Vector3 start_pos, end_pos;
    public Vector3 sizeCells;
    public bool canMove = true;

    public int targetRotation;
    public Transform playerCamera;

    private int height = 20;
    private int width = 20;

    private int[,] _grid; 

    Transform _transform;
    public DW_PlayerSound playerSound;



    //int[,] Grid = { {1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1 },  // 1 = world border
    //                {1,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,1 },  // 0 = innaccessibility
    //                {1,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,1 },  // 5 = character
    //                {1,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,1 },  // 6 = spawn
    //                {1,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,1 },  // 2 = path
    //                {1,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,1 }, // 3 = ennemy
    //                {1,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,1 },
    //                {1,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,1 },
    //                {1,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,1 },
    //                {1,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,1 },
    //                {1,2,2,2,2,2,2,2,2,2,5,2,2,2,2,2,2,2,2,1 },
    //                {1,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,1 },
    //                {1,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,1 },
    //                {1,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,1 },
    //                {1,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,1 },
    //                {1,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,1 },
    //                {1,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,1 },
    //                {1,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,1 },
    //                {1,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,1 },
    //                {1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1 }};



    public string Rotation;

    [SerializeField]int CharacterX;
    [SerializeField]int CharacterY;

    private void Start()
    {
        _transform = gameObject.transform;
        _grid = DW_GridMap.Instance.Grid;
        SetFirst();
        GiveDirectionByRotation();

    }
    //private void ConvertList(int[,] _grid)
    //{
    //    for (int i = 0; i < 20; i++)
    //    {
    //        _grid.Add(new List<int>());
    //        for (int j = 0; j < 20; j++)
    //        {
    //            _grid[i].Add(_grid[i, j]);
    //        }
    //    }
    //}


    private void SetFirst()
    {
        DW_GridMap.Instance.Spawn(ID, new Vector2Int((int)gameObject.transform.position.x, (int)gameObject.transform.position.z));
        CharacterX = Mathf.Abs((int)gameObject.transform.position.x / 10);
        CharacterY = Mathf.Abs((int)gameObject.transform.position.z/10);
    }

    //private Vector2Int GetCharacterOnGrid()
    //{
    //    for (int i = 0; i < _grid.Length; i++)
    //    {
    //        for (int j = 0; j < _grid.Length; j++)
    //        {
    //            if (_grid[i,j] == 5)
    //            {
    //                return new Vector2Int(j, i);

    //            }
    //        }
    //    }
    //    return  Vector2Int.zero;
    //}

    private Vector2Int GetCharacterPos()
    {
        return new Vector2Int(CharacterX, CharacterY);
    }


    private IEnumerator CharacterMove(float total_time)
    {
        canMove = false;
        float time = 0f;
        start_pos = _transform.position;
        end_pos = _transform.position + sizeCells;
        if(playerSound != null)
            playerSound.PlayerSteps();

        // If gameObject is the player, play cam anim
        if(playerCamera != null) 
            playerCamera.GetComponent<Animation>().Play();


        while (time / total_time < 1)
        {
            time += Time.deltaTime;
            _transform.position = Vector3.Lerp(start_pos, end_pos, time / total_time);

            yield return null;
        }
        yield return new WaitForSeconds(waitCooldown);
        canMove = true;
    }

    public void StartCharacterMove(float total_time)
    {
        if (canMove && CheckArround())
        {
            StartCoroutine(CharacterMove(total_time));
            GridMove();
        }
    }
    

    private IEnumerator CharacterTurn(float total_time, bool direction, bool sameAxis)
    {
        targetRotation = 0;
        canMove = false;
        float time = 0f;
        float rotation;
        float initialRotation = _transform.eulerAngles.y;
        if (sameAxis)
        {
            targetRotation = Mathf.RoundToInt(initialRotation + 180);
        }
        else
            targetRotation = Mathf.RoundToInt(direction ? initialRotation + 90f : initialRotation - 90f);
        Debug.Log(targetRotation);

        GiveDirectionByRotation();

        while (time / total_time < 1)
        {
            time += Time.deltaTime;
            rotation = Mathf.Lerp(initialRotation, targetRotation, time / total_time);

            _transform.rotation = Quaternion.Euler(0, rotation, 0);
            yield return null;
        }
        yield return new WaitForSeconds(waitCooldown);
        canMove = true;
    }

    private void GiveDirectionByRotation()
    {
        if (targetRotation == 360 || targetRotation == 0)
        {
            Debug.Log("UP");
            sizeCells = new Vector3(0, 0, 10);
            Rotation = "Up";

        }
        // Move player right
        else if (targetRotation == 90 || targetRotation == 450)
        {
            sizeCells = new Vector3(10, 0, 0);
            Rotation = "Right";

        }
        //Move player left
        else if (targetRotation == 270 || targetRotation == -90)
        {
            sizeCells = new Vector3(-10, 0, 0);
            Rotation = "Left";
        }
        //Move player behind
        else if (targetRotation == 180 || targetRotation == -180)
        {
            sizeCells = new Vector3(0, 0, -10);
            Rotation = "Down";
        }
    }

    private void GridMove()
    {
        switch (Rotation)
        {

            case "Left":
                DW_GridMap.Instance.SetMyPosInGrid(ID, GetCharacterPos(), new Vector2Int(CharacterX - 1, CharacterY));
                    CharacterX -= 1;
                break;
            case "Right":
                DW_GridMap.Instance.SetMyPosInGrid(ID, GetCharacterPos(), new Vector2Int(CharacterX + 1, CharacterY));
                CharacterX += 1;
                break;
            case "Up":
                DW_GridMap.Instance.SetMyPosInGrid(ID, GetCharacterPos(), new Vector2Int(CharacterX, CharacterY-1));
                CharacterY -= 1;
                break;
            case "Down":
                DW_GridMap.Instance.SetMyPosInGrid(ID, GetCharacterPos(), new Vector2Int(CharacterX, CharacterY+1));
                CharacterY += 1;
                break;
            default:
                break;
        }
    }

    public void StartCharacterTurn(float total_time, bool direction, bool sameAxis)
    {
        if (canMove)
        {
            StartCoroutine(CharacterTurn(total_time, direction, sameAxis));
        }
    }
    private bool CheckArround()
    {
        switch (Rotation)
        {

            case "Left":
                if (_grid[CharacterY,CharacterX - 1] == 2)
                {
                    Debug.Log(_grid[CharacterY,CharacterX - 1]);
                    return true;
                }
                else
                {
                    return false;
                }
            case "Right":
                if (_grid[CharacterY,CharacterX + 1] == 2)
                {

                    Debug.Log(_grid[CharacterY,CharacterX + 1]);
                    return true;
                }
                else
                {
                    return false;
                }
            case "Up":
                if (_grid[CharacterY - 1,CharacterX] == 2)
                {

                    Debug.Log(_grid[CharacterY - 1,CharacterX]);
                    return true;
                }
                else
                {
                    return false;
                }
            case "Down":
                if (_grid[CharacterY + 1,CharacterX] == 2)
                {

                    Debug.Log(_grid[CharacterY + 1,CharacterX]);
                    Debug.Log("Down");
                    return true;
                }
                else
                {
                    return false;
                }
            default:
                break;
        }
        return false;
    }

    public List<Vector2> neighbors = new List<Vector2>();

    public List<Vector2> GetPathAround(Vector2 position)
    {
        neighbors.Clear();
        //List<Vector2> neighbors = new List<Vector2>();

        int X = (int)position.x;
        int Y = (int)position.y;

        int current_X = 0;
        int current_Y = 0;

        int[] around = new int[2] { -1,1};

        for(int i = 0; i < around.Length; i++)
        {
            current_X = X + around[i];
            current_Y = Y + around[i];
            if (current_X >= 0 && current_X < width)
            {
                if (_grid[current_X, Y] == 2 || _grid[current_X, Y] == 5)
                {
                    neighbors.Add(new Vector2(current_X, Y));
                }
            }

            if (current_Y >= 0 && current_Y < height)
            {
                if (_grid[X,current_Y] == 2 || _grid[X, current_Y] == 5)
                {
                    neighbors.Add(new Vector2(X, current_Y));
                }
            }
        }


        return neighbors;
    }

    public void UpdatePos(Vector2Int initialPos, Vector2Int newPos)
    {
        _grid[initialPos.x,initialPos.y] = 2;
        _grid[newPos.x,newPos.y] = 5;
    }


    public Vector2 GetPos()
    {
        return GetCharacterPos();
    }
}
