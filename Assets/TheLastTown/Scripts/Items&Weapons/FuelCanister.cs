using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FuelCanister : Item
{
    [SerializeField] protected float fuelAmount;

    public override void UseItem(Soldier soldier)
    {
        soldier.inventory.WeaponOwner.ForEach(w =>
        {
            if (w.Type == WeaponType.Flamethrower)
            {
                Flamethrower flamethrower = w as Flamethrower;
                flamethrower.RefillFuel();
            }
        });
    }
}
