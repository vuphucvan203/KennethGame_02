using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MedicalKit : Item
{
    [SerializeField] protected int healthAmount;

    public override void UseItem(Soldier soldier)
    {
        soldier.Health.IncreaseStats(healthAmount);
    }
}
