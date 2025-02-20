using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor.Toolbars;
using UnityEngine;
using UnityEngine.UI;

public class WeaponChange : MonoBehaviour
{
    public TMP_Dropdown weaponDropdown;
    public WeaponType selectedWeapon;

    private void Start()
    {
        weaponDropdown.onValueChanged.AddListener(OnSelectedWeapon);
    }

    protected void OnSelectedWeapon(int index)
    {
        switch(index)
        {
            case 0:
                selectedWeapon = WeaponType.Knife;
                break;
            case 1:
                selectedWeapon = WeaponType.Bat;
                break;
            case 2:
                selectedWeapon = WeaponType.Gun;
                break;
            case 3:
                selectedWeapon = WeaponType.MachineGun;
                break;
            case 4:
                selectedWeapon = WeaponType.Flamethrower;
                break;
        }    
    }
}
