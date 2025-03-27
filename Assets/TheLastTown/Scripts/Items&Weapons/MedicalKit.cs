using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MedicalKit : Item
{
    [SerializeField] protected int healthAmount;

    public override void UseItem(Player player)
    {
        player.controller.Soldier.Health.IncreaseStats(healthAmount);
    }
}
