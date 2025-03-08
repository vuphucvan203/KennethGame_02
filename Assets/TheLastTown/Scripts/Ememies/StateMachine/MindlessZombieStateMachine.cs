using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MindlessZombieStateMachine : EnemyStateMachine
{
    public MindlessZombieType type;

    protected override void Awake()
    {
        base.Awake();
        LoadStateHandle(type + "MindlessZombie");
    }
}
