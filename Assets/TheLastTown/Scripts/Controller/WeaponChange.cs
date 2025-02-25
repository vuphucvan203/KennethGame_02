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
    public bool isChanged;

    private void Start()
    {
        weaponDropdown.onValueChanged.AddListener(OnSelectedWeapon);
    }

    protected void OnSelectedWeapon(int index)
    {
        isChanged = true;
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
                selectedWeapon = WeaponType.Riffle;
                break;
            case 4:
                selectedWeapon = WeaponType.Flamethrower;
                break;
        }    
    }
}
