using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;


public class Flamethrower : Weapon
{
    [SerializeField] protected float fuel;
    [SerializeField] protected float tankCapacity;
    [SerializeField] protected float fuelConsumptionRate;
    [SerializeField] protected int fireRate;

    public override void EquipWeapon(Player player)
    {
        player.controller.cooldownAttack = 1/ fireRate;
    }

    public void RefillFuel()
    {
        fuel = tankCapacity;
    }

    public override void UseWeapon(Player player)
    {
        if (fuel > 0)
        {
            float fuelConsumption = fuel * fuelConsumptionRate;
            if (fuelConsumption > fuel) fuel = 0;
            else fuel -= fuelConsumption;
        }
        else Debug.Log("Fuel is empty!");
    }
}
