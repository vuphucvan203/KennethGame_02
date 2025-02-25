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
        switch (weaponType)
        {
            case WeaponType.Knife:
                state = "Walk_knife";
                break;
            case WeaponType.Bat:
                state = "Walk_bat";
                break;
            case WeaponType.Gun:
                state = "Walk_gun";
                break;
            case WeaponType.Riffle:
                state = "Walk_riffle";
                break;
            case WeaponType.Flamethrower:
                state = "Walk_flamethrower";
                break;
        }
        stateMachine.Animator.CrossFade(state, 0.2f);
        stateMachine.Controller.idleTrigger += IdleHandle;
        stateMachine.Controller.attackTrigger += AttackHandle;
    }

    public void Excute()
    {
        
    }

    public void Exit()
    {
        stateMachine.Controller .idleTrigger -= IdleHandle;
        stateMachine.Controller .attackTrigger -= AttackHandle;
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
