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
    private Animator anim;


    public canSeePlayer(GameObject enemy, GameObject player, float viewDistance, DW_AiMovement _movement, Animator animator)
    {
        Enemy = enemy;
        Player = player;
        view_distance = viewDistance;   
        movement = _movement;
        anim = animator;
    }

    public override NodeState Evaluate()
    {
        //Vector3 heading = Player.transform.position - Enemy.transform.position;
        //if (Physics.Raycast(Enemy.transform.position, heading / heading.magnitude, out hit, view_distance))
        //{
        //    Debug.DrawRay(Enemy.transform.position, heading / heading.magnitude * view_distance, Color.red);
        //}
        //if (hit.collider != null && hit.collider.tag == "Player")
        //{
        //    if(!movement.IsPathNull())
        //    {
        //        movement.Path.Clear();
        //    }
        //    return NodeState.RUNNING;
        //}

        if (Vector3.Distance(Enemy.transform.position, Player.transform.position) < view_distance)
            return NodeState.SUCCESS;
        else
        {
            anim.SetBool("IsWalking", false);
        }

        return NodeState.FAILURE;
    }

    public void SetFieldOfView(float viewDist)
    {
        view_distance = viewDist;
    }
}
