using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AiAgent : MonoBehaviour
{
    public AiStateID initialState;
    public AiAgent_config config;
    public BoxCollider weaponCollider;
    [HideInInspector] public AiStateMacine stateMacine;
    [HideInInspector] public NavMeshAgent navMeshAgent;
    [HideInInspector] public RagDoll ragDoll;
    [HideInInspector] public Transform player;
    [HideInInspector] public Transform healthLoco;
    [HideInInspector] public Animator animator;
    [HideInInspector] public WeaponIK weaponIK;
    [HideInInspector] public AiWeapon aiWeapon;
    [HideInInspector] public AiSensor aiSensor;
    [HideInInspector] public AIHealth aIHealth;
    [HideInInspector] public AiTargetingSystem targeting;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag(config.playerTag).transform;
        
        navMeshAgent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        ragDoll = GetComponent<RagDoll>();
        aiWeapon = GetComponent<AiWeapon>();
        weaponIK = GetComponent<WeaponIK>();
        aiSensor = GetComponent<AiSensor>();
        aIHealth = GetComponent<AIHealth>();
        targeting = GetComponent<AiTargetingSystem>();
        stateMacine = new AiStateMacine(this);
        stateMacine.RegisterState(new AiChasePlayer());
        stateMacine.RegisterState(new AiDeadState());
        stateMacine.RegisterState(new AiStateIdle());
        stateMacine.RegisterState(new AiFindWeapon());
        stateMacine.RegisterState(new AiFindTargetState());
        stateMacine.RegisterState(new AiAttackTarget());
        stateMacine.ChangeState(initialState);
    }
    private void Update()
    {
        stateMacine.Update();
    }

}
