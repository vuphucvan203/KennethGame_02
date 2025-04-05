using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScrollWeapon : KennMonoBehaviour
{
    [SerializeField] protected ScrollRect scrollRect;
    [SerializeField] protected List<RectTransform> weapons;
    public WeaponType selectedWeapon;
    protected int selectedWeaponIndex;
    public bool isChanged;

    protected override void Start()
    {
        base.Start();
    }

    private void Update()
    {
        FindSelectedWeapon();
    }

    protected override void LoadComponent()
    {
        base.LoadComponent();
        scrollRect = GetComponent<ScrollRect>();
        for (int i = 0; i < scrollRect.content.childCount; i++)
        {
            RectTransform weapon = scrollRect.content.GetChild(i).GetComponent<RectTransform>();
            if (weapons.Contains(weapon)) return;
            weapons.Add(weapon);
        }
    }

    protected void FindSelectedWeapon()
    {
        float minDistance = float.MaxValue;
        int selectedIndex = 0;
        for (int i = 0; i < weapons.Count; i++)
        {
            float weaponPos = scrollRect.content.anchoredPosition.x + weapons[i].anchoredPosition.x;
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
            if (i == selectedWeaponIndex) weapons[i].localScale = new Vector2(1.7f, 1.7f);
            else weapons[i].localScale = Vector2.one;
        }

        switch(selectedWeaponIndex)
        {
            case 0:
                selectedWeapon = WeaponType.Riffle;
                break;
            case 1:
                selectedWeapon = WeaponType.Gun;
                break;
            case 2:
                selectedWeapon = WeaponType.Knife;
                break;
            case 3:
                selectedWeapon = WeaponType.Bat;
                break;
            case 4:
                selectedWeapon = WeaponType.Flamethrower;
                break;
        }
    }    
}
