using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AcidSpitter))]
[RequireComponent(typeof(AcidSpitterStateMachine))]
[RequireComponent(typeof(EnemyStateTrigger))]
public class AcidSpitterAI : EnemyAI
{
    protected override void MakeDecision()
    {
        base.MakeDecision();
    }
}
