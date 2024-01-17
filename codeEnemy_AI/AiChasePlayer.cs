using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class AiChasePlayer : AiState
{

    [HideInInspector] public float timer = 0.0f;

    public AiStateID GetID()
    {
        return AiStateID.ChasePlayer;
    }

    public void Enter(AiAgent agent)
    {
    }
    public void Update(AiAgent agent)
    {
        agent.navMeshAgent.stoppingDistance = agent.config.stoppingDistance;
        if (!agent.enabled)
        {
            return;
        }
       timer -=Time.deltaTime;
       if (!agent.navMeshAgent.hasPath)
        {
            agent.navMeshAgent.destination = agent.player.position;
        }
        if (agent.navMeshAgent.hasPath)
        {
            agent.stateMacine.ChangeState(AiStateID.AttackTarget); 
        }
        if (timer < 0.0f)
        {
            Vector3 direction = (agent.player.position - agent.navMeshAgent.destination);
            direction.y = 0;
            if (direction.sqrMagnitude > agent.config.maxDistance * agent.config.maxDistance)
            {
               if(agent.navMeshAgent.pathStatus != NavMeshPathStatus.PathPartial)
                {
                    agent.navMeshAgent.destination = agent.player.position;
                }
            }
            timer = agent.config.maxTime;
        }
    }
    public void Exit(AiAgent agent)
    {

    }

   

}
