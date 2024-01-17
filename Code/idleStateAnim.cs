using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class idleStateAnim : StateMachineBehaviour
{
    float timer;
    NavMeshAgent agent;
    Transform player;
    Health health;
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        timer = 0;
        player = GameObject.FindGameObjectWithTag("Player").transform;
        agent = animator.GetComponent<NavMeshAgent>();
        health = animator.GetComponent<Health>();
    }

    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        ZombieOpiton zombieOpiton = animator.GetComponent<ZombieOpiton>();
        if (zombieOpiton != null)
        {
            timer += Time.deltaTime;
            
            float Distance = Vector3.Distance(player.position, animator.transform.position);
            if (Distance < zombieOpiton.enemyConfig.chaseRange)
            {
                animator.SetBool("isChasing", true);
            }
            if (zombieOpiton.playerAttacked)
            {
                UpdateChasing();
                animator.SetBool("isChasing", true);
            }
            else
            {
                animator.SetBool("isPatrolling", true);
            }
        }
    }

    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {

    }

    override public void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        // Implement code that processes and affects root motion
    }

    override public void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        // Implement code that sets up animation IK (inverse kinematics)
    }
    void UpdateChasing()
    {
        agent.SetDestination(player.position);

    }
}
