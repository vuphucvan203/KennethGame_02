using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScrollWeapon : KennMonoBehaviour
{
    [SerializeField] protected ScrollRect scrollRect;
    [SerializeField] protected List<Image> weapons;
    [SerializeField] protected List<Weapon> weaponEquip;
    public WeaponType selectedWeapon;
    protected int selectedWeaponIndex;
    public bool isChanged;

    protected override void Start()
    {
        base.Start();
    }

    private void Update()
    {
        
        UpdateWeaponStorage();
        FindSelectedWeapon();
    }

    protected override void LoadComponent()
    {
        base.LoadComponent();
        scrollRect = GetComponent<ScrollRect>();
        for (int i = 0; i < scrollRect.content.childCount; i++)
        {
            Image weapon = scrollRect.content.GetChild(i).GetComponent<Image>();
            if (weapons.Contains(weapon)) return;
            weapons.Add(weapon);
        }
    }

    protected void UpdateWeaponStorage()
    {
        weaponEquip = Player.Instance.controller.Soldier.inventory.WeaponOwner;
        foreach (var weapon in weaponEquip)
        {
            switch (weapon.Type)
            {
                case WeaponType.Knife:
                    weapons[0].enabled = true;
                    break;
                case WeaponType.Bat:
                    weapons[1].enabled = true;
                    break;
                case WeaponType.Gun:
                    weapons[2].enabled = true;
                    break;
                case WeaponType.Riffle:
                    weapons[3].enabled = true;
                    break;
                case WeaponType.Flamethrower:
                    weapons[4].enabled = true;
                    break;
            }
        }
    }    

    protected void FindSelectedWeapon()
    {
        float minDistance = float.MaxValue;
        int selectedIndex = 0;
        for (int i = 0; i < weapons.Count; i++)
        {
            float weaponPos = scrollRect.content.anchoredPosition.x + weapons[i].rectTransform.anchoredPosition.x;
            float distance = Mathf.Abs(scrollRect.content.rect.width / 2 - weaponPos);
            if (distance < minDistance)
            {
                minDistance = distance;
                selectedIndex = i;
            }
        }

        if (selectedIndex != selectedWeaponIndex)
        {
            selectedWeaponIndex = selectedIndex;
            isChanged = true;
        }    
        for (int i = 0; i < weapons.Count; i++)
        {
            if (i == selectedWeaponIndex) weapons[i].rectTransform.localScale = new Vector2(2, 2);
            else weapons[i].rectTransform.localScale = Vector2.one;
        }

        WeaponType type = selectedWeapon;
        switch(selectedWeaponIndex)
        {
            case 0:
                type = WeaponType.Knife;
                break;
            case 1:
                type = WeaponType.Bat;
                break;
            case 2:
                type = WeaponType.Gun;
                break;
            case 3:
                type = WeaponType.Riffle;
                break;
            case 4:
                type = WeaponType.Flamethrower;
                break;
        }

        Weapon weapon = weaponEquip.Find(x => x.Type == type);
        if (weapon != null)
        {
            selectedWeapon = weapon.Type;
        }
    }    
}
