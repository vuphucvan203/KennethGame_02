using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AcidSpitterStateMachine : EnemyStateMachine
{
    public AcidSpitterType type;

    protected override void Awake()
    {
        base.Awake();
        LoadStateHandle(type + "AcidSpitter");
    }
}
