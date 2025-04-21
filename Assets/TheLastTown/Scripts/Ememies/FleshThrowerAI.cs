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

    protected override void HandleAttackStrategy()
    {
        GetRandomAttack();
        base.HandleAttackStrategy();
    }

    protected void GetRandomAttack()
    {
        int strategyIndex = Random.Range(0, 99);
        if (strategyIndex <= 54) attack = AttackType.Melee;
        else attack = AttackType.Stun;
    }
}
