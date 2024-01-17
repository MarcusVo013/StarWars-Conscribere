using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AIEnemyCode : MonoBehaviour
{
    public NavMeshAgent agent;
    public Transform player;
    public LayerMask IsGround, Isplayer;
    public GameObject ShotObject;
    [SerializeField] private string PlayerObject = "Player";

    //Patroling State
    [SerializeField] private Vector3 WalkP;//Walk Point
    [SerializeField] private float WalkR;//Walk RAnge
    private bool SetWP;//Set Walk Point.

    //Attacking State
    [SerializeField] float AtkT;//Attack Time.
    bool AAtk;//Already Attack.
    //Idle State
    [SerializeField] float sightRange, AtkR;//Atack Range.
    bool PlayerInRange, PlayerInAtkR;

    private void Awake()
    {
        player = GameObject.Find(PlayerObject).transform;
        agent = GetComponent<NavMeshAgent>();
    }
    private void Update()
    {
        //CheckbPlayer In Range
        PlayerInRange = Physics.CheckSphere(transform.position, sightRange, Isplayer);
        PlayerInAtkR = Physics.CheckSphere(transform.position, AtkR, Isplayer);
        if(!PlayerInRange && !PlayerInAtkR) Patroling();
        if (PlayerInRange && !PlayerInAtkR) ChasePlayer();
        if (PlayerInRange && PlayerInAtkR) Attack();
    }
    private void SearchWalkPoint()
    {
        //Random Point In Range
        float randomZ = Random.Range(-WalkR, WalkR);
        float randomX = Random.Range(-WalkR, WalkR);
        WalkP = new Vector3(transform.position.x + randomX ,transform.position.y,transform.position.z+randomZ);
        if (Physics.Raycast(WalkP,-transform.up,2f, IsGround)) { SetWP = true; }
    }
    private void ChasePlayer()
    {
        agent.SetDestination(player.position);
    }
    private void Patroling()
    {
        if (!SetWP) SearchWalkPoint();
        if (SetWP)
            agent.SetDestination(WalkP);
        Vector3 distanceToWalkPoint= transform.position - WalkP;

        //WalkPoint Reached
        if (distanceToWalkPoint.magnitude < 1f)
            SetWP = false;
    }

    private void Attack()
    {
        //Enemy Stop Moving
        agent.SetDestination(transform.position);
        transform.LookAt(player);
        if (!AAtk)
        {
            Rigidbody rb = Instantiate(ShotObject, transform.position , Quaternion.identity).GetComponent<Rigidbody>();
            
            rb.AddForce(transform.forward * 32f , ForceMode.Impulse);
            rb.AddForce(transform.up * 32f, ForceMode.Impulse);
            AAtk = true;
            Invoke(nameof(ResetAtk), AtkT);
        }
    }
    private void ResetAtk()
    {
        AAtk=false;
    }
}
