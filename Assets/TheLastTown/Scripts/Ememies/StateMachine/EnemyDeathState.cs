using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDeathState : EnemyBaseState, IState
{
    public EnemyDeathState(EnemyStateMachine stateMachine) : base(stateMachine)
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
