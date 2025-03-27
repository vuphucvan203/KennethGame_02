using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class MeleeWeapon : Weapon
{
    [SerializeField] protected int attackSpeed;

    public override void EquipWeapon(Player player)
    {
        player.controller.cooldownAttack = 1 / attackSpeed;
    }

    public override void UseWeapon(Player player)
    {
        throw new System.NotImplementedException();
    }
}
