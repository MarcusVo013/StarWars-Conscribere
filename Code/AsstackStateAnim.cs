using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsstackStateAnim : StateMachineBehaviour
{
    Transform player;
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        ZombieOpiton zombieOpiton = animator.GetComponent<ZombieOpiton>();
        if (zombieOpiton != null)
        {
            animator.transform.LookAt(player);
            float Distance = Vector3.Distance(player.position, animator.transform.position);
            if (Distance > zombieOpiton.enemyConfig.attackRange)
            {                
                animator.SetBool("isAttacking", false);
            }
        }
        if (player.GetComponent<AIHealth>().Isdead())
        {
            animator.SetBool("isAttacking", false);
        }
    }

    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        
    }
}
