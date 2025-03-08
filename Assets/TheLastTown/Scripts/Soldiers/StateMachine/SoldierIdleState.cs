using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoldierIdleState : SoldierBaseState, IState
{
    protected WeaponType weaponType;
    protected string state;

    public SoldierIdleState(WeaponType type, SoldierStateMachine stateMachine) : base(stateMachine)
    {
        weaponType = type;
    }

    public void Enter()
    {
        stateMachine.Soldider.SetStategy(weaponType);
        stateMachine.Soldider.IdleStrategy.SelectStrategy(stateMachine.Soldider);
        stateMachine.Soldider.StateTrigger.moveTrigger += MoveHandle;
        stateMachine.Soldider.StateTrigger.attackTrigger += AttackHandle;
    }

    public void Excute()
    {
        if (stateMachine.Soldider.Controller.changeWeapon)
        {
            stateMachine.Soldider.Controller.changeWeapon = false;
            stateMachine.Soldider.StateTrigger.idleTrigger += ChangeWeapon;
        }
    }

    public void Exit()
    {
        stateMachine.Soldider.StateTrigger.moveTrigger -= MoveHandle;
        stateMachine.Soldider.StateTrigger.attackTrigger -= AttackHandle;
        stateMachine.Soldider.StateTrigger.idleTrigger -= ChangeWeapon;
    }

    void MoveHandle(WeaponType weaponType)
    {
        stateMachine.SwitchState(new SoldierMoveState(weaponType, stateMachine));
    }
    void AttackHandle(WeaponType weaponType)
    {
        stateMachine.SwitchState(new SoldierAttackState(weaponType, stateMachine));
    }

    void ChangeWeapon(WeaponType weaponType)
    {
        stateMachine.SwitchState(new SoldierIdleState(weaponType, stateMachine));
    } 
}
