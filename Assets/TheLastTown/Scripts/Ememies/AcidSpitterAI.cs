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

    protected override void HandleAttackStrategy()
    {
        GetRandomAttack();
        base.HandleAttackStrategy();
    }

    protected void GetRandomAttack()
    {
        int strategyIndex = Random.Range(0, 98);
        if (strategyIndex <= 32) attack = AttackType.Melee;
        else if (strategyIndex >= 33 && strategyIndex <= 65) attack = AttackType.SmallAcid;
        else attack = AttackType.BigAcid;
    }    
}
