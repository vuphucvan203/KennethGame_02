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
    [SerializeField] protected Sprite sprite;
    public Sprite Sprite => sprite;
    [SerializeField] protected ItemType type;
    public ItemType Type => type;
    [SerializeField] protected int price;
    public int Price => price;
    public bool isPickupable = false;
    public bool startDelay;
    public float timer;

    protected void Update()
    {
        if (startDelay) timer += Time.deltaTime;
        if (timer > 0.1f)
        {
            isPickupable = true;
            startDelay = false;
            timer = 0;
        }
    }

    public string GetInfor()
    {
        return type.ToString();
    }

    public abstract void UseItem(Soldier soldier);
}
