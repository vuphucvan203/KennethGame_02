using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoldierMoveState : SoldierBaseState, IState
{
    protected WeaponType weaponType;
    protected string state;

    public SoldierMoveState(WeaponType type, SoldierStateMachine stateMachine) : base(stateMachine)
    {
        weaponType = type;
    }

    public void Enter()
    {
        stateMachine.Soldider.SetStategy(weaponType);
        stateMachine.Soldider.MoveStrategy.SelectStrategy(stateMachine.Soldider);
        stateMachine.Soldider.StateTrigger.idleTrigger += IdleHandle;
        stateMachine.Soldider.StateTrigger.attackTrigger += AttackHandle;
    }

    public void Excute()
    {
        
    }

    public void Exit()
    {
        stateMachine.Soldider.StateTrigger .idleTrigger -= IdleHandle;
        stateMachine.Soldider.StateTrigger .attackTrigger -= AttackHandle;
    }

    void IdleHandle(WeaponType weaponType)
    {
        stateMachine.SwitchState(new SoldierIdleState(weaponType, stateMachine));
    }
    
    void AttackHandle(WeaponType weaponType)
    {
        stateMachine.SwitchState(new SoldierAttackState(weaponType, stateMachine));
    }
}
