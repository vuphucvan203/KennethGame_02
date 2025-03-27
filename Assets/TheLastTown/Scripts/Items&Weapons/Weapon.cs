using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public abstract class Weapon : KennMonoBehaviour
{
    [SerializeField] protected WeaponType type;
    [SerializeField] protected int damage;

    public string GetInfor()
    {
        return type.ToString();
    }

    public abstract void EquipWeapon(Player player);

    public abstract void UseWeapon(Player player);
}
