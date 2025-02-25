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
        switch (weaponType)
        {
            case WeaponType.Knife:
                state = "Attack_knife";
                break;
            case WeaponType.Bat:
                state = "Attack_bat";
                break;
            case WeaponType.Gun:
                state = "Attack_gun";
                break;
            case WeaponType.Riffle:
                state = "Attack_riffle";
                break;
            case WeaponType.Flamethrower:
                state = "Attack_flamethrower";
                break;
        }
        stateMachine.Animator.CrossFade(state, 0.2f);
        stateMachine.Controller.moveTrigger += MoveHandle;
    }

    public void Excute()
    {
        if (stateMachine.Controller.finishAttack)
        {
            stateMachine.Controller.finishAttack = false;
            stateMachine.Controller.idleTrigger += IdleHandle;
        }

    }

    public void Exit()
    {
        stateMachine.Controller .idleTrigger -= IdleHandle;
        stateMachine.Controller .moveTrigger -= MoveHandle;
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
