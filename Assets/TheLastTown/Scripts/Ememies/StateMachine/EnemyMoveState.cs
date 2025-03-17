using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMoveState : EnemyBaseState, IState
{
    public EnemyMoveState(EnemyStateMachine stateMachine) : base(stateMachine)
    {

    }

    public void Enter()
    {
        State move = stateMachine.Move[0];
        stateMachine.Animator.Play(move.name);
        //stateMachine.Animator.CrossFade(move.name, move.duringTime);
        stateMachine.Enemy.StateTrigger.AttackTrigger += AttackHandle;
    }

    public void Excute()
    {
        
    }

    public void Exit()
    {
        stateMachine.Enemy.StateTrigger.AttackTrigger -= AttackHandle;
    }

    protected void AttackHandle()
    {
        stateMachine.SwitchState(new EnemyAttackState(stateMachine.Enemy.EnemyAI.AttackType ,stateMachine));
    }
}
