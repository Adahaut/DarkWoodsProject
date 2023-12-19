using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Node_script;

public class DW_Attack : Node
{
    // get la class actuel du joueur et lui appliquer des dégats (apres chaque coups nodestate = failure pendant x temps
    private GameObject Player;
    private GameObject Enemy;
    private float time_between_attacks;
    private float last_attack_time;


    public DW_Attack(GameObject enemy, GameObject player, float timeBetweenAttacks)
    {
        Enemy = enemy;
        Player = player;
        time_between_attacks = timeBetweenAttacks;
    }  

    public override NodeState Evaluate()
    {
        if (Time.time - last_attack_time >= time_between_attacks)
        {
            Player.GetComponent<DW_LifeManager>().TakeDamage(Enemy.GetComponent<DW_LifeManager>().damage);

            last_attack_time = Time.time;

            Debug.Log("Attack Player");
            return NodeState.SUCCESS;
        }
        else
        {
            Debug.Log("Off attack");
            return NodeState.RUNNING;
        }
    }


}
