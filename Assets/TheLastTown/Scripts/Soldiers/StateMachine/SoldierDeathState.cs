using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoldierDeathState : SoldierBaseState, IState
{
    public SoldierDeathState(SoldierStateMachine stateMachine) : base(stateMachine)
    {

    }

    public void Enter()
    {
        State death = stateMachine.Death[0];
        stateMachine.Animator.Play(death.name);
    }

    public void Excute()
    {
        
    }

    public void Exit()
    {

    }
}
