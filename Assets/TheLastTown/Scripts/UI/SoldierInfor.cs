using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using static UnityEditor.PlayerSettings;

public class SoldierInfor : KennMonoBehaviour
{
    [SerializeField] protected Slider fuel;
    [SerializeField] protected RectTransform gunMagazine;
    [SerializeField] protected RectTransform riffleMagazine;
    [SerializeField] protected List<Image> gunAmmos;
    [SerializeField] protected List<Image> riffleAmmos;
    [SerializeField] protected List<Image> shield;
    [SerializeField] protected List<Sprite> ammoSprites;
    [SerializeField] protected List<Sprite> shieldSprites;

    protected void Update()
    {
        UpdateWeaponSelecting();
    }

    protected override void LoadComponent()
    {
        base.LoadComponent();
        fuel = transform.Find("Fuel").GetComponent<Slider>();
        gunMagazine = transform.Find("GunMagazine").GetComponent<RectTransform>();
        riffleMagazine = transform.Find("RiffleMagazine").GetComponent<RectTransform>();
        gunAmmos = transform.Find("GunMagazine").GetComponentsInChildren<Image>().ToList();
        riffleAmmos = transform.Find("RiffleMagazine").GetComponentsInChildren<Image>().ToList();
        shield = transform.Find("ShieldContainer").GetComponentsInChildren<Image>().ToList();
        ammoSprites = Resources.LoadAll<Sprite>("Sprite/Ammo").ToList();
        shieldSprites = Resources.LoadAll<Sprite>("Sprite/Shield").ToList();
    }

    protected void UpdateWeaponSelecting()
    {
        switch (Player.Instance.controller.ScrollWeapon.selectedWeapon)
        {
            case WeaponType.Knife:
            case WeaponType.Bat:
                gunMagazine.gameObject.SetActive(false);
                riffleMagazine.gameObject.SetActive(false);
                fuel.gameObject.SetActive(false);
                break;
            case WeaponType.Gun:
                gunMagazine.gameObject.SetActive(true);
                riffleMagazine.gameObject.SetActive(false);
                fuel.gameObject.SetActive(false);
                break;
            case WeaponType.Riffle:
                gunMagazine.gameObject.SetActive(false);
                riffleMagazine.gameObject.SetActive(true);
                fuel.gameObject.SetActive(false);
                break;
            case WeaponType.Flamethrower:
                gunMagazine.gameObject.SetActive(false);
                riffleMagazine.gameObject.SetActive(false);
                fuel.gameObject.SetActive(true);
                break;
        }

        UpdateMagazine(WeaponType.Gun, gunAmmos);
        UpdateMagazine(WeaponType.Riffle, riffleAmmos);
        UpdateFuelCanister();
    }    

    protected void UpdateMagazine(WeaponType type, List<Image> ammos)
    {
        List<Weapon> weapons = Player.Instance.controller.Soldier.inventory.WeaponOwner;
        foreach (var weapon in weapons)
        {
            if (weapon.Type == type)
            {
                BulletGun bulletGun = (BulletGun)weapon;
                int ammoCount = bulletGun.Ammo;
                for (int i = 0; i < ammos.Count; i++)
                {
                    if (i < ammoCount)
                    {
                        ammos[i].sprite = ammoSprites[0];
                    }
                    else
                    {
                        ammos[i].sprite = ammoSprites[1];
                    }
                }
            }
        }
    }

    protected void UpdateFuelCanister()
    {
        List<Weapon> weapons = Player.Instance.controller.Soldier.inventory.WeaponOwner;
        foreach (var weapon in weapons)
        {
            if (weapon.Type == WeaponType.Flamethrower)
            {
                Flamethrower flamethrower = (Flamethrower)weapon;
                fuel.value = flamethrower.PercentRemainFuel;
            }
        }
    }    
}
