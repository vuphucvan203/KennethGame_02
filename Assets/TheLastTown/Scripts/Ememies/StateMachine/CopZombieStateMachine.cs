using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CopZombieStateMachine : EnemyStateMachine
{

    protected override void Awake()
    {
        base.Awake();
        LoadStateHandle("CopZombie");
    }
}
