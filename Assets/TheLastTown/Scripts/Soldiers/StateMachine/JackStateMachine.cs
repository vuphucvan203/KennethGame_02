using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JackStateMachine : SoldierStateMachine
{
    protected override void Awake()
    {
        base.Awake();
        LoadStateHandle("Jack");
    }
}
