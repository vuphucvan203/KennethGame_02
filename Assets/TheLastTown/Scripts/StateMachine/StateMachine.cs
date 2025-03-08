using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class StateMachine : KennMonoBehaviour
{
    IState currentState;
    [SerializeField] protected Animator animator;
    public Animator Animator => animator;

    public State[] Idle;
    public State[] Move;
    public State[] Attack;
    public State[] Death;

    protected virtual void Awake()
    {
        animator = GetComponentInChildren<Animator>();
    }

    private void Update()
    {
        if(currentState != null)
        {
            currentState.Excute();
        }
    }

    protected void LoadStateHandle(string character)
    {
        StateHandle stateHandle = Resources.Load<StateHandle>("State/" + character + "State");
        Idle = stateHandle.Idle;
        Move = stateHandle.Move;
        Attack = stateHandle.Attack;
        Death = stateHandle.Death;
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
