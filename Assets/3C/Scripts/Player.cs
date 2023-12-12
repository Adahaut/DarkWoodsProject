using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Security.Cryptography;
using Unity.PlasticSCM.Editor.WebApi;
using Unity.VisualScripting;
using Unity.VisualScripting.Dependencies.Sqlite;
using UnityEngine;
using static UnityEditor.FilePathAttribute;

public class Player : MonoBehaviour
{
    public float waitCooldown = 0.5f;
    private Vector3 start_pos, end_pos;
    public Vector3 sizeCells;
    public bool canMove = true;

    public int targetRotation;
    public Transform playerCamera;

    Transform _transform;


    
    int[,] Grid = { {1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1 },  // 1 = world border
                    {1,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,1 },  // 0 = innaccessibility
                    {1,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,1 },  // 5 = player
                    {1,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,1 },  // 6 = spawn
                    {1,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,1 },  // 2 = path
                    {1,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,1 },
                    {1,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,1 },
                    {1,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,1 },
                    {1,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,1 },
                    {1,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,1 },
                    {1,5,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,1 },
                    {1,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,1 },
                    {1,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,1 },
                    {1,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,1 },
                    {1,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,1 },
                    {1,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,1 },
                    {1,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,1 },
                    {1,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,1 },
                    {1,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,1 },
                    {1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1 }};
    


    string Rotation;

    List<List<int>> gridList;

    int PlayerX;
    int PlayerY;

    private void Start()
    {
        gridList = new List<List<int>>();
        _transform = transform;
        ConvertList(Grid);
        GetPlayerPos(gridList);

    }
    private void ConvertList(int[,] _grid)
    {
        for (int i = 0; i < 20 ; i++)
        {
            gridList.Add(new List<int>());
            for (int j = 0; j < 20 ; j++)
            {
                gridList[i].Add(_grid[i, j]);
            }
        }
    }

    private void GetPlayerPos(List<List<int>> _grid) 
    { 
        for (int i=0; i < _grid.Count  ; i++ )
        {
            for (int j = 0; j < _grid.Count; j++)
            {
                if (_grid[i][j] == 5)
                {
                    PlayerX = j;
                    PlayerY = i;
                    return;
                     
                }              
            }
        }
    }


    private IEnumerator PlayerMove(float total_time)
    {
        canMove = false;
        float time = 0f;
        start_pos = _transform.position;
        end_pos = _transform.position + sizeCells;

        //Play steps sound
        playerCamera.gameObject.GetComponent<DW_PlayerSound>().PlayerSteps();

        // a remettre sur branche originale
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

    public void StartPlayerMove(float total_time)
    {
        if (canMove && CheckArround(gridList))
        {
            StartCoroutine(PlayerMove(total_time));
        }
    }
    /*
     * 
     */
    private IEnumerator PlayerTurn(float total_time, bool direction, bool sameAxis)
    {
        targetRotation = 0;
        canMove = false;
        float time = 0f;
        float rotation;
        float initialRotation = _transform.eulerAngles.y;
        if (sameAxis)
        {
            targetRotation =Mathf.RoundToInt( initialRotation + 180);
        }
        else
            targetRotation = Mathf.RoundToInt( direction ? initialRotation + 90f : initialRotation -90f);
        Debug.Log(targetRotation);

        if (targetRotation == 360 || targetRotation == 0)
        {
            Debug.Log("UP");
            sizeCells = new Vector3(0, 0, 10);
            Rotation = "Up";

        }
        // Move player right
        else if (targetRotation == 90|| targetRotation == 450)
        {
            sizeCells = new Vector3(10, 0, 0);
            Rotation = "Right";

        }
        //Move player left
        else if (targetRotation == 270|| targetRotation == -90)
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

        while (time / total_time < 1)
        {
            time += Time.deltaTime;
            rotation = Mathf.Lerp (initialRotation, targetRotation, time / total_time);

            _transform.rotation = Quaternion.Euler(0, rotation, 0);
            yield return null;
        }
        yield return new WaitForSeconds(waitCooldown);
        canMove = true;
    }
    public void StartPlayerTurn(float total_time, bool direction, bool sameAxis)
    {
        if (canMove)
        {
            StartCoroutine(PlayerTurn(total_time, direction , sameAxis));
        }
    }
    private bool CheckArround(List<List<int>> _grid)
    {
        switch (Rotation)
        {
          
            case "Left":
                if (_grid[PlayerY][ PlayerX - 1] == 2)
                {
                    Debug.Log(_grid[PlayerY][PlayerX - 1]);
                    PlayerX -= 1;
                    return true;
                }
                else
                {
                    return false;
                }
            case "Right":
                if (_grid[PlayerY][PlayerX + 1] == 2)
                {

                    Debug.Log(_grid[PlayerY][PlayerX + 1]);
                    PlayerX += 1;
                    return true;
                }
                else
                {
                    return false;
                }
            case "Up":
                if (_grid[PlayerY - 1 ][PlayerX ] == 2)
                {

                    Debug.Log(_grid[PlayerY - 1][PlayerX]);
                    PlayerY -=  1;
                    return true;
                }
                else
                {
                    return false;
                }
            case "Down":
                if (_grid[PlayerY + 1 ][PlayerX ] == 2)
                {

                    Debug.Log(_grid[PlayerY + 1][PlayerX]);
                    Debug.Log("Down");
                    PlayerY += 1;
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
}
