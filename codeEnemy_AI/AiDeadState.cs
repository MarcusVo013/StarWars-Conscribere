using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AiDeadState : AiState
{
    public Vector3 direction;
    public AiStateID GetID()
    {
        return AiStateID.Dead;
    }

    public void Enter(AiAgent agent)
    {
        agent.ragDoll.RagdollOn();
        direction.y = 1;
        agent.ragDoll.ApplyForce(direction * agent.config.dieForce);
        agent.weaponIK.SetTargetTranform(null);
        agent.aiWeapon.enabled = false;
        agent.weaponCollider.enabled = true;
    }
    public void Update(AiAgent agent)
    {
        
    }
    public void Exit(AiAgent agent)
    {

    }
}
