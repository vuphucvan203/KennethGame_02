using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(FleshThrower))]
[RequireComponent(typeof(FleshThrowerStateMachine))]
[RequireComponent(typeof(EnemyStateTrigger))]
public class FleshThrowerAI : EnemyAI
{
    protected override void MakeDecision()
    {
        base.MakeDecision();
    }
}
