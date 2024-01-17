using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AiFindTargetState : AiState
{
    public void Enter(AiAgent agent)
    {
        
    }
    public void Update(AiAgent agent)
    {
        if (!agent.navMeshAgent.hasPath)
        {
            WorldBound worldBound = GameObject.FindObjectOfType<WorldBound>();
            agent.navMeshAgent.destination = worldBound.RandomPosition();
        }
        //if (agent.targeting.HasTarget)
        //{
        //    agent.aiWeapon.SetTarget(agent.targeting.target.transform);
        //    agent.stateMacine.ChangeState(AiStateID.AttackTarget);
        //}
    }

    public void Exit(AiAgent agent)
    {
        
    }

    public AiStateID GetID()
    {
        return AiStateID.FindTarget;
    }


 
}
