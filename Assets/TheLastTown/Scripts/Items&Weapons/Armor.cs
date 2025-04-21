using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Armor : Item
{
    [SerializeField] protected int defenseAmount;

    public override void UseItem(Soldier soldier)
    {
        soldier.DefenseStats.IncreaseStats(defenseAmount);
    }
}
