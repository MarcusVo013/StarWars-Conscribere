using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AiStateMacine
{
    public AiState[] states;
    public AiAgent agent;
    public AiStateID currentState;
    public AiStateMacine(AiAgent agent)
    {
        this.agent = agent;
        int numState = System.Enum.GetNames(typeof(AiStateID)).Length;
        states = new AiState[numState];
    }
    public void RegisterState(AiState state)
    {
        int index = (int)state.GetID();
        states[index] = state;
    }
    public AiState GetState(AiStateID stateid)
    {
        int index = (int)stateid;
        return states[index];
    }
    public void Update()
    {
        GetState(currentState)?.Update(agent);
    }
    public void ChangeState(AiStateID newState)
    {
        if (currentState != newState)
        {
            GetState(currentState)?.Exit(agent);
            currentState = newState;
            GetState(currentState)?.Enter(agent);
        }
    }
}
