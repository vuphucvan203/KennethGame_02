using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoldierStateMachine : StateMachine
{
    public PlayerController Controller;
    public Animator Animator;

    private void Awake()
    {
        Controller = GetComponent<PlayerController>();
        Animator = GetComponentInChildren<Animator>();
    }

    void Start()
    {
        Controller.idleTrigger += IdleHandle;
    }

    protected void IdleHandle(WeaponType type)
    {
        Controller.idleTrigger -= IdleHandle;
        StartState(new SoldierIdleState(type, this));
    }    
}
