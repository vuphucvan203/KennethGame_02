using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Money : Item
{
    public override void UseItem(Soldier soldier)
    {
        soldier.Money += price;
    }
}
