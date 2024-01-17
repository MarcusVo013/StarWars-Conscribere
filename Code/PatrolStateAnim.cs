using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PatrolStateAnim : StateMachineBehaviour 
{
    List<Transform> wayPoints = new List<Transform>();
    NavMeshAgent agent;
    float timer;
    Transform player;
    WorldBound worldBound;

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        worldBound = animator.GetComponent<WorldBound>();
        
        timer = 0;
        
    }

    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        ZombieOpiton zombieOpiton = animator.GetComponent<ZombieOpiton>();
        worldBound = animator.GetComponent<WorldBound>();
        NavMeshAgent agent = animator.GetComponent<NavMeshAgent>();
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
                agent.SetDestination(player.position);
                animator.SetBool("isChasing", true);
            }
            else
            {
                if (agent.remainingDistance <= agent.stoppingDistance)
                {
                    agent.speed = 0.2f;
                    agent.destination = worldBound.RandomPosition();
                    if (zombieOpiton.playerAttacked)
                    {
                        agent.SetDestination(player.position);
                        animator.SetBool("isChasing", true);
                        GameObject.FindGameObjectWithTag("Zombie").GetComponent<ZombieOpiton>().UpdateTargetPlayerPosition();
                    }
                }

            }
        }
    }

    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        
    }

    //override public void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that processes and affects root motion
    //}

    //override public void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that sets up animation IK (inverse kinematics)
    //}
}
