using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements.Experimental;
using static Node_script; 

public class canSeePlayer : Node
{
    private RaycastHit hit;
    private GameObject Enemy;
    private GameObject Player;
    private float view_distance;
    public canSeePlayer(GameObject enemy, GameObject player, float viewDistance)
    {
        Enemy = enemy;
        Player = player;
        view_distance = viewDistance;   
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
