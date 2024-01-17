using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AiAttackTarget: AiState
{
    

    public void Enter(AiAgent agent)
    {

    }
    public void Exit(AiAgent agent)
    {
        agent.navMeshAgent.stoppingDistance = 0.0f;
    }

    public AiStateID GetID()
    {
        return AiStateID.AttackTarget;
    }

    public void Update(AiAgent agent)
    {
        agent.navMeshAgent.speed = agent.config.chaseSpeed;
        agent.navMeshAgent.destination = agent.player.transform.position;
        agent.navMeshAgent.stoppingDistance = agent.config.stoppingDistance;
        float distanceToPlayer = Vector3.Distance(agent.transform.position, agent.player.position);
        if (distanceToPlayer <= agent.config.stoppingDistance && distanceToPlayer < agent.config.fireDistance)
        {
            Firing(agent);
            UpdateFiring(agent);
            agent.navMeshAgent.transform.LookAt(agent.player.position);
        }
        else
        {
            agent.aiWeapon.SetFiring(false);
        }
        if(agent.player.GetComponent<PlayerHealth>().Isdead())
        {
            agent.aiWeapon.SetFiring(false);
        }
        //if (agent.aIHealth.IslowHealth())
        //{
        //    agent.aiWeapon.SetFiring(false);
        //    agent.stateMacine.ChangeState(AiStateID.FindHealth);
        //}
    }

    private void UpdateFiring(AiAgent agent)
    {
        if(agent.player) 
        {
            agent.aiWeapon.SetFiring(true);
        }
        else
        {
            agent.aiWeapon.SetFiring(false);
        }
    }

    void Firing(AiAgent agent)
    {
        agent.aiWeapon.SetTarget(agent.player);
        agent.aiWeapon.ActivateWeapon();
        agent.aiWeapon.SetFiring(true);
    }
}
