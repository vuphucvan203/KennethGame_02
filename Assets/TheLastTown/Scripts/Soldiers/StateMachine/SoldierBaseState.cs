using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class SoldierBaseState 
{
    [SerializeField] protected SoldierStateMachine stateMachine;

    public SoldierBaseState(SoldierStateMachine soldierStateMachine)
    {
        stateMachine = soldierStateMachine;
    }
}
