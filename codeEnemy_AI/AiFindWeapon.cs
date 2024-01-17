using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AiFindWeapon    : AiState
{
    GameObject pickUp;
    GameObject[] pickups = new GameObject[1];
    public void Enter(AiAgent agent)
    {
        pickUp = null;
    }

    public void Exit(AiAgent agent)
    {
        
    }

    public AiStateID GetID()
    {
        return AiStateID.FindHealth;
    }

    public void Update(AiAgent agent)
    {
        if (agent.aIHealth.IslowHealth())
        {
            agent.healthLoco = GameObject.FindGameObjectWithTag("Health").transform;
            agent.navMeshAgent.stoppingDistance = 0;
            agent.navMeshAgent.destination = agent.healthLoco.position;
        }
        if (agent.aiWeapon.HasWeapon() && !agent.aIHealth.IslowHealth())
        {           
            agent.stateMacine.ChangeState(AiStateID.ChasePlayer);            
        }
        //Random Walking
        if (!agent.navMeshAgent.hasPath)
        {
            WorldBound worldBound = GameObject.FindObjectOfType<WorldBound>();           
            agent.navMeshAgent.destination = worldBound.RandomPosition();
        }
    }   
    void CollectPickUp(AiAgent agent, GameObject gameObject)
    {
        agent.navMeshAgent.destination = pickUp.transform.position;
    }
}

