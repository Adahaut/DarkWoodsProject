using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DW_DirtyIA : MonoBehaviour
{
    public bool can_move = true;
    private float current_time = 0.5f;

    private Vector3[] path_taken;

    private void OnEnable()
    {
        FindDestination(GameObject.Find("Player").transform.position);
    }

    private void Update()
    {
        if(can_move)
        {
            var dist = Vector3.Distance(this.transform.position, GameObject.Find("Player").transform.position);

            if(dist > 10)
            {
                FindDestination(GameObject.Find("Player").transform.position);
                Debug.Log(dist);
            }
        }
        if(current_time < 0)
        {
            can_move = true;
            current_time = 0.5f;
        }
        else
            current_time -= Time.deltaTime;


    }
    private void FindDestination(Vector3 _goal)
    {
        if( can_move)
        {
            Vector3 dest = Vector3.zero;

            var dir_x = _goal.x - this.transform.position.x;
            var dir_z = _goal.z - this.transform.position.z;


            if (dir_x > dir_z)
            {
                dest = new Vector3((dir_x/dir_x) * 10, 0, 0);
            }
            else
            {
                dest = new Vector3(0, 0, (dir_z / dir_z) * 10);
            }

            Debug.Log(dest);
            if (dest != Vector3.zero)
            {
                StartCoroutine(GoToDestination(dest));
            }
        }
        
    }

    IEnumerator GoToDestination(Vector3 destination)
    {
        this.transform.position += destination;
        yield return can_move = false;
    }
}
