using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoldierAttackState : SoldierBaseState, IState
{
    protected WeaponType weaponType;
    protected string state;

    public SoldierAttackState(WeaponType type, SoldierStateMachine stateMachine) : base(stateMachine)
    {
        weaponType = type;
    }

    public void Enter()
    {
        stateMachine.Soldider.SetStategy(weaponType);
        stateMachine.Soldider.AttackStrategy.SelectStrategy(stateMachine.Soldider);
        stateMachine.Soldider.StateTrigger.moveTrigger += MoveHandle;
    }

    public void Excute()
    {
        if (stateMachine.Soldider.StateTrigger.finishAttack)
        {
            stateMachine.Soldider.StateTrigger.finishAttack = false;
            stateMachine.Soldider.StateTrigger.idleTrigger += IdleHandle;
        }

    }

    public void Exit()
    {
        stateMachine.Soldider.StateTrigger .idleTrigger -= IdleHandle;
        stateMachine.Soldider.StateTrigger .moveTrigger -= MoveHandle;
    }

    void IdleHandle(WeaponType weaponType)
    {
        stateMachine.SwitchState(new SoldierIdleState(weaponType, stateMachine));
    }
    
    void MoveHandle(WeaponType weaponType)
    {
        stateMachine.SwitchState(new SoldierMoveState(weaponType, stateMachine));
    }
}
