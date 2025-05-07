using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Magazine : Item
{
    [SerializeField] protected int ammoAmount;

    public override void UseItem(Soldier soldier)
    {
        soldier.inventory.WeaponOwner.ForEach(w =>
        {
            if (w.Type == WeaponType.Gun && type == ItemType.GunAmmo)
            {
                BulletGun gun = w as BulletGun;
                gun.Reload();
            }
            else if (w.Type == WeaponType.Riffle && type == ItemType.RiffleAmmo)
            {
                BulletGun riffle = w as BulletGun;
                riffle.Reload();
            }
        });
    }
}
