using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum AiStateID
{
    ChasePlayer,
    Dead,
    Idle,
    FindHealth,
    AttackTarget,
    FindTarget,
}
public interface AiState 
{
    AiStateID GetID();
    void Enter (AiAgent agent);
    void Update(AiAgent agent);
    void Exit(AiAgent agent);
}
