using UnityEngine;
using UnityEngine.AI;

public class ChasingStateAnim : StateMachineBehaviour
{
    NavMeshAgent agent;
    Transform player;
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        ZombieOpiton zombieOpiton = animator.GetComponent<ZombieOpiton>();
        NavMeshAgent agent = animator.GetComponent<NavMeshAgent>();
        if (zombieOpiton != null)
        {
            player = GameObject.FindGameObjectWithTag("Player").transform;
            agent = animator.GetComponent<NavMeshAgent>();
            agent.speed = zombieOpiton.enemyConfig.chaseSpeed;
        }
    }

    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        ZombieOpiton zombieOpiton = animator.GetComponent<ZombieOpiton>();
        NavMeshAgent agent = animator.GetComponent<NavMeshAgent>();
        if (zombieOpiton != null)
        {
            agent.SetDestination(player.position);
            agent.speed = zombieOpiton.enemyConfig.chaseSpeed;
            float Distance = Vector3.Distance(player.position, animator.transform.position);
            if (Distance <= zombieOpiton.enemyConfig.attackRange)
            {
                animator.SetBool("isAttacking", true);
            }
            if (player.GetComponent<AIHealth>().Isdead())
            {
                animator.SetBool("isPatrolling", true);
            }

        }
    }

    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        
    }
    
}
