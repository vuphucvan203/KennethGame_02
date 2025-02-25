using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class StateMachine : MonoBehaviour
{
    IState currentState;

    private void Update()
    {
        if(currentState != null)
        {
            currentState.Excute();
        }
    }

    protected void StartState(IState state)
    {
        currentState = state;
        currentState.Enter();
    }    

    public void SwitchState(IState state)
    {
        currentState.Exit();
        currentState = state;
        state.Enter();
    }    
}
