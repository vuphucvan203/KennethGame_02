using System.Collections;
using System.Collections.Generic;
using UnityEditorInternal;
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
    SuperPower,
    Money
}

public abstract class Item : KennMonoBehaviour
{
    [SerializeField] protected ItemType type;
    public ItemType Type => type;
    [SerializeField] protected int price;
    public bool isPickupable = false;

    public string GetInfor()
    {
        return type.ToString();
    }

    public abstract void UseItem(Soldier soldier);
}
