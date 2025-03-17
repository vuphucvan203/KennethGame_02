using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class EnemyAttackState : EnemyBaseState, IState
{
    protected AttackType attackType;

    public EnemyAttackState(AttackType type, EnemyStateMachine stateMachine) : base(stateMachine)
    {
        attackType = type;
    }

    public void Enter()
    {
        stateMachine.Enemy.SetStrategy(attackType);
        stateMachine.Enemy.AttackStrategy.SelectStrategy(stateMachine.Enemy);
        stateMachine.Enemy.StateTrigger.MoveTrigger += MoveHandle;
    }

    public void Excute()
    {
        
    }

    public void Exit()
    {
        stateMachine.Enemy.StateTrigger.MoveTrigger -= MoveHandle;
    }

    protected void MoveHandle()
    {
        stateMachine.SwitchState(new EnemyMoveState(stateMachine));
    }
}
