using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedUp : Item
{
    [SerializeField] protected int upAmount;

    public override void UseItem(Player player)
    {
        player.controller.Soldier.SpeedStats.IncreaseStats(upAmount);
    }
}
