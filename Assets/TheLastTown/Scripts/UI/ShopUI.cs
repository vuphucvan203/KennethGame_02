using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ShopUI : KennMonoBehaviour
{
    [SerializeField] protected TextMeshProUGUI money;
    [SerializeField] protected Item medicalKit;
    [SerializeField] protected Item gunAmmo;
    [SerializeField] protected Item riffleAmmo;
    [SerializeField] protected Item Armor;
    [SerializeField] protected Item fuelCanister;
    [SerializeField] protected TextMeshProUGUI medicalKitPrice;
    [SerializeField] protected TextMeshProUGUI gunAmmoPrice;
    [SerializeField] protected TextMeshProUGUI riffleAmmoPrice;
    [SerializeField] protected TextMeshProUGUI armorPrice;
    [SerializeField] protected TextMeshProUGUI fuelCanisterPrice;

    protected void Update()
    {
        money.SetText(Player.Instance.controller.Soldier.Money.ToString());
        medicalKitPrice.SetText(medicalKit.Price.ToString());
        gunAmmoPrice.SetText(gunAmmo.Price.ToString());
        riffleAmmoPrice.SetText(riffleAmmo.Price.ToString());
        armorPrice.SetText(Armor.Price.ToString());
        fuelCanisterPrice.SetText(fuelCanister.Price.ToString());
    }

    protected override void LoadComponent()
    {
        base.LoadComponent();
        money = transform.Find("Money").GetComponentInChildren<TextMeshProUGUI>();
        medicalKit = Resources.Load<Item>("Prefabs/Item/MedicalKit");
        gunAmmo = Resources.Load<Item>("Prefabs/Item/GunAmmo");
        riffleAmmo = Resources.Load<Item>("Prefabs/Item/RiffleAmmo");
        Armor = Resources.Load<Item>("Prefabs/Item/Armor");
        fuelCanister = Resources.Load<Item>("Prefabs/Item/FuelCanister");
        medicalKitPrice = transform.Find("Container/MedicalKit").GetComponentInChildren<TextMeshProUGUI>();
        gunAmmoPrice = transform.Find("Container/GunAmmo").GetComponentInChildren<TextMeshProUGUI>();
        riffleAmmoPrice = transform.Find("Container/RiffleAmmo").GetComponentInChildren<TextMeshProUGUI>();
        armorPrice = transform.Find("Container/Armor").GetComponentInChildren<TextMeshProUGUI>();
        fuelCanisterPrice = transform.Find("Container/FuelCanister").GetComponentInChildren<TextMeshProUGUI>();
    }

    public void BuyMedicalKit()
    {
        if (Player.Instance.controller.Soldier.Money < medicalKit.Price) return;
        Player.Instance.controller.Soldier.Money -= medicalKit.Price;
        medicalKit.isPickupable = true;
        Player.Instance.controller.Soldier.inventory.AddItem(medicalKit, 1);
    }    

    public void BuyGunAmmo()
    {
        if (Player.Instance.controller.Soldier.Money < gunAmmo.Price) return;
        Player.Instance.controller.Soldier.Money -= gunAmmo.Price;
        gunAmmo.isPickupable = true;
        Player.Instance.controller.Soldier.inventory.AddItem(gunAmmo, 1);
    }

    public void BuyRiffleAmmo()
    {
        if (Player.Instance.controller.Soldier.Money < riffleAmmo.Price) return;
        Player.Instance.controller.Soldier.Money -= riffleAmmo.Price;
        riffleAmmo.isPickupable = true;
        Player.Instance.controller.Soldier.inventory.AddItem(riffleAmmo, 1);
    }

    public void BuyArmor()
    {
        if (Player.Instance.controller.Soldier.Money < Armor.Price) return;
        Player.Instance.controller.Soldier.Money -= Armor.Price;
        Armor.isPickupable = true;
        Player.Instance.controller.Soldier.inventory.AddItem(Armor, 1);
    }   
    
    public void BuyFuelCanister()
    {
        if (Player.Instance.controller.Soldier.Money < fuelCanister.Price) return;
        Player.Instance.controller.Soldier.Money -= fuelCanister.Price;
        fuelCanister.isPickupable = true;
        Player.Instance.controller.Soldier.inventory.AddItem(fuelCanister, 1);
    }    
}
