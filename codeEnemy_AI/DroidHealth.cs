using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DroidHealth : AIHealth
{
    AiAgent aiAgent;
    protected override void OnStart()
    {
        aiAgent = GetComponent<AiAgent>();
    }
    protected override void OnDead(Vector3 direction)
    {
        AiDeadState deadState = aiAgent.stateMacine.GetState(AiStateID.Dead) as AiDeadState;
        deadState.direction = direction;
        aiAgent.stateMacine.ChangeState(AiStateID.Dead);
        Destroy(this.gameObject, 4f);
    }
    protected override void OnDamage(Vector3 direction)
    {

    }
    protected override void OnHeal(float amount)
    {

    }
}
