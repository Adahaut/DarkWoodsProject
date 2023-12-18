using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using static Node_script;
using static Selector;
using static Sequence;
using static canSeePlayer;
using static DW_AiMovement;

public class BehaviorTree_script : MonoBehaviour
{
    private RaycastHit hit;

    private Selector selectorRoot;
    private Sequence Chase;
    private Selector ReturnToIniTialPos;
    [SerializeField]private GameObject Player;
    [SerializeField] private float view_distance  = 20;
    [SerializeField] private float attack_range  = 10;
    [SerializeField] private float time_between_attacks = 3;
    private bool canAttack = false;

    private canSeePlayer _canSeePlayer;
    private DW_AiMovement _aiMovement;
    private DW_VerifPosition _VerifPos;
    private DW_PurchasePlayer _purchasePlayer;
    private DW_ReturnToIniTialPos _returnToIniTialPos;
    private DW_CanAttack _canAttack;
    private DW_Attack _attack;
    // Start is called before the first frame update
    void Start()
    {
        Player = GameObject.Find("Player");
        _aiMovement = gameObject.GetComponent<DW_AiMovement>();

        _VerifPos = new DW_VerifPosition(gameObject.GetComponent<DW_Character>().initial_pos, gameObject.GetComponent<DW_Character>());
        _canSeePlayer = new canSeePlayer(gameObject, Player,  view_distance, _aiMovement);
        _returnToIniTialPos = new DW_ReturnToIniTialPos(_aiMovement, gameObject.GetComponent<DW_Character>(), gameObject.GetComponent<DW_Character>().initial_pos);
        _purchasePlayer = new DW_PurchasePlayer(_aiMovement, gameObject.GetComponent<DW_Character>(), Player.GetComponent<DW_Character>());
        _canAttack = new DW_CanAttack(gameObject, Player, attack_range);
        _attack = new DW_Attack(gameObject, Player, time_between_attacks);



        //Chase = new Sequence(new List<Node> { _canSeePlayer, _purchasePlayer });
        //ReturnToIniTialPos = new Selector(new List<Node> { _VerifPos, _returnToIniTialPos });
        //selectorRoot = new Selector(new List<Node> { Chase/*, ReturnToIniTialPos*/ });

        Sequence sequence1 = new Sequence(new List<Node> { _canAttack, _attack });
        Selector selector1 = new Selector(new List<Node> { sequence1, _purchasePlayer });
        Sequence sequence2 = new Sequence(new List<Node> { _canSeePlayer, selector1 });
        Selector selector2 = new Selector(new List<Node> { _VerifPos, _returnToIniTialPos });
        selectorRoot = new Selector(new List<Node> { /*sequence2*/ sequence2, selector2 });

    }

    // Update is called once per frame
    void Update()
    {
        _canSeePlayer.SetFieldOfView(view_distance);
        NodeState a  = selectorRoot.Evaluate();
        Debug.Log(a.ToString());
        //Vector3 heading = Player.transform.position - gameObject.transform.position;
        //Debug.DrawRay(gameObject.transform.position, heading / heading.magnitude * 30, Color.green);
        //if (Physics.Raycast(gameObject.transform.position, heading / heading.magnitude, out hit, 30))
        //{
        //    float angleOfVision = Vector3.Angle(-Player.transform.forward, heading / heading.magnitude);

        //    if (hit.collider != null && hit.collider.tag == "Player" && angleOfVision < 45 )
        //    {
        //        if(!iSeeHim)
        //        {
        //            iSeeHim = true;
        //            deathTimer = Time.time;
        //        }
        //        Debug.DrawRay(gameObject.transform.position, heading / heading.magnitude * 30, Color.blue);
        //        if (Time.time - deathTimer > 3)
        //        {
        //            Player.GetComponent<Ennemies_count>().count++;
        //            gameObject.SetActive(false);
        //        }
        //    }
        //    else
        //    {
        //        iSeeHim = false;
        //        deathTimer = Time.time;
        //    }

        //}
    }
}
