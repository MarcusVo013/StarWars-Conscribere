using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AiStateIdle : AiState
{
    public void Enter(AiAgent agent)
    {
        agent.stateMacine.ChangeState(AiStateID.FindHealth);
    }
    public void Exit(AiAgent agent)
    {
        
    }
    public AiStateID GetID()
    {
        return AiStateID.Idle;
    }
    public void Update(AiAgent agent)
    {
        Vector3 playerDirection = agent.player.position - agent.transform.position;
        if(playerDirection.magnitude > agent.config.maxSightDistance)
        {
            return;
        }
        Vector3 agentDirection = agent.transform.forward;
        playerDirection.Normalize();
        float dotProduct = Vector3.Dot(playerDirection, agentDirection);
        if(dotProduct >0.0f)
        { agent.stateMacine.ChangeState(AiStateID.ChasePlayer); }       
    }
}
