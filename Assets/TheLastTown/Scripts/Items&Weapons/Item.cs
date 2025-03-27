using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ItemType
{
    MedicalKit,
    Armor,
    GunAmmo,
    RiffleAmmo,
    FuelCanister,
    SpeedUp,
    SlowDown,
    SuperPower
}

public abstract class Item : KennMonoBehaviour
{
    [SerializeField] protected ItemType type;
    [SerializeField] protected int price;

    public string GetInfor()
    {
        return type.ToString();
    }

    public abstract void UseItem(Player player);
}
