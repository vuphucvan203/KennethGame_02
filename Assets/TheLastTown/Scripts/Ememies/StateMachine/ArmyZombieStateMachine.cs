using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArmyZombieStateMachine : EnemyStateMachine
{
    protected override void Awake()
    {
        base.Awake();
        LoadStateHandle("ArmyZombie");
    }
}
