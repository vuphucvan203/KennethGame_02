using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Money : Item
{
    public bool startDelay;
    public float timer;

    protected void Update()
    {
        if (startDelay) timer += Time.deltaTime;
        if (timer > 0.5f)
        {
            isPickupable = true;
            startDelay = false;
            timer = 0;
        }
    }

    public override void UseItem(Soldier soldier)
    {
        soldier.Money += price;
    }
}
