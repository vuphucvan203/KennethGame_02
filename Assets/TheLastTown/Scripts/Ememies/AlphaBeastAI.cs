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

    protected override void HandleAttackStrategy()
    {
        GetRandomAttack();
        base.HandleAttackStrategy();
    }

    protected void GetRandomAttack()
    {
        int strategyIndex = Random.Range(0, 99);
        if (strategyIndex <= 24) attack = AttackType.Melee;
        else if (strategyIndex >= 25 && strategyIndex <= 49) attack = AttackType.BloodTalon;
        else if (strategyIndex >= 50 && strategyIndex <= 74) attack = AttackType.TwinTalons;
        else attack = AttackType.Fire;
    }
}
