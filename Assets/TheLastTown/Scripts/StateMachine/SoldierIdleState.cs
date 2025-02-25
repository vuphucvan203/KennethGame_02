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
        switch(weaponType)
        {
            case WeaponType.Knife:
                state = "Idle_knife";
                break;
            case WeaponType.Bat:
                state = "Idle_bat";
                break;
            case WeaponType.Gun:
                state = "Idle_gun";
                break;
            case WeaponType.Riffle:
                state = "Idle_riffle";
                break;
            case WeaponType.Flamethrower:
                state = "Idle_flamethrower";
                break;
        }
        stateMachine.Animator.CrossFade(state, 0.2f);
        stateMachine.Controller.moveTrigger += MoveHandle;
        stateMachine.Controller.attackTrigger += AttackHandle;
    }

    public void Excute()
    {
        if (stateMachine.Controller.WeaponChange.isChanged)
        {
            stateMachine.Controller.WeaponChange.isChanged = false;
            stateMachine.Controller.idleTrigger += ChangeWeapon;
        }
    }

    public void Exit()
    {
        stateMachine.Controller.moveTrigger -= MoveHandle;
        stateMachine.Controller.attackTrigger -= AttackHandle;
        stateMachine.Controller.idleTrigger -= ChangeWeapon;
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
