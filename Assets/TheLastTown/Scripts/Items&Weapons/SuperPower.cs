
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SuperPower : Item
{
    [SerializeField] protected int attackAmount;

    public override void UseItem(Player player)
    {
        player.controller.Soldier.AttackStats.IncreaseStats(attackAmount);
    }
}
