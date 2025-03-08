using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LindaStateMachine : SoldierStateMachine
{
    protected override void Awake()
    {
        base.Awake();
        LoadStateHandle("Linda");
    }
}
