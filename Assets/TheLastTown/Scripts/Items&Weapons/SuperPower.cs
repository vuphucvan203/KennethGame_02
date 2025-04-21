
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SuperPower : Item
{
    [SerializeField] protected int attackAmount;

    public override void UseItem(Soldier soldier)
    {
        soldier.AttackStats.IncreaseStats(attackAmount);
    }
}
