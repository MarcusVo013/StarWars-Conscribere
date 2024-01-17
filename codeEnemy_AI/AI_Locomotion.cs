using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AI_Locomotion : MonoBehaviour
{
    [SerializeField] string Move_Parameter_Animator = "Speed";
    NavMeshAgent agent;
    Animator animator;

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        if (agent.hasPath)
        {
            animator.SetFloat(Move_Parameter_Animator, agent.velocity.magnitude);
        }
        else
        {
            animator.SetFloat (Move_Parameter_Animator, 0f);
        }
    }
}
