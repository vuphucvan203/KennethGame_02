using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FleshThrowerStateMachine : EnemyStateMachine
{
    public FleshThrowerType type;

    protected override void Awake()
    {
        base.Awake();
        LoadStateHandle(type + "FleshThrower");
    }
}
