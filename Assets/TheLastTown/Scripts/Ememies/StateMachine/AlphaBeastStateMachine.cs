using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlphaBeastStateMachine : EnemyStateMachine
{
    protected override void Awake()
    {
        base.Awake();
        LoadStateHandle("AlphaBeast");
    }
}
