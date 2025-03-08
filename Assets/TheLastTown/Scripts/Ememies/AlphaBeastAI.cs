using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AlphaBeast))]
[RequireComponent(typeof(AlphaBeastStateMachine))]
[RequireComponent(typeof(EnemyStateTrigger))]
public class AlphaBeastAI : EnemyAI
{
    protected override void MakeDecision()
    {
        base.MakeDecision();
    }
}
