using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UIElements.Experimental;
using static Node_script; 

public class canSeePlayer : Node
{
    private RaycastHit hit;
    private GameObject Enemy;
    private GameObject Player;
    private float view_distance;
    private DW_AiMovement movement;


    public canSeePlayer(GameObject enemy, GameObject player, float viewDistance, DW_AiMovement _movement)
    {
        Enemy = enemy;
        Player = player;
        view_distance = viewDistance;   
        movement = _movement;
    }

    public override NodeState Evaluate()
    {
        Vector3 heading = Player.transform.position - Enemy.transform.position;
        if (Physics.Raycast(Enemy.transform.position, heading / heading.magnitude, out hit, view_distance))
        {
            Debug.DrawRay(Enemy.transform.position, heading / heading.magnitude * view_distance, Color.red);
        }
        if (hit.collider != null && hit.collider.tag == "Player")
        {
            if(!movement.IsPathNull())
            {
                movement.Path.Clear();
            }

            Debug.Log("See Player");
            return NodeState.SUCCESS;
        }

        return NodeState.FAILURE;
    }

    public void SetFieldOfView(float viewDist)
    {
        view_distance = viewDist;
    }
}
