using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class BulletGun : Weapon
{
    [SerializeField] protected int ammo;
    [SerializeField] protected int magazineSize;
    [SerializeField] protected int fireRate;
    [SerializeField] protected int ammoPerShot;

    public override void EquipWeapon(Player player)
    {
        player.controller.cooldownAttack = 1 / fireRate;
    }

    public void Reload()
    {
        ammo = magazineSize;
    }

    public override void UseWeapon(Player player)
    {
        if (ammo > 0)
        {
            if (ammo < ammoPerShot) ammo = 0;
            else ammo -= ammoPerShot;
        }
        else Debug.Log("Ammo is empty!");
    }
}
