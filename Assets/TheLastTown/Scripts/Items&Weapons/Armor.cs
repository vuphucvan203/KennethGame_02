using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Armor : Item
{
    [SerializeField] protected int defenseAmount;

    public override void UseItem(Player player)
    {
        player.controller.Soldier.DefenseStats.IncreaseStats(defenseAmount);
    }
}
