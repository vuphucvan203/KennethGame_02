using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class SoldierStateMachine : StateMachine
{
    [SerializeField] protected Soldier soldider;
    public Soldier Soldider => soldider;
    
    protected override void Awake()
    {
        base.Awake();
        soldider = GetComponent<Soldier>();
    }

    protected override void Start()
    {
        soldider.StateTrigger.idleTrigger += IdleHandle;
    }

    protected void IdleHandle(WeaponType type)
    {
        soldider.StateTrigger.idleTrigger -= IdleHandle;
        StartState(new SoldierIdleState(type, this));
    }    
}
