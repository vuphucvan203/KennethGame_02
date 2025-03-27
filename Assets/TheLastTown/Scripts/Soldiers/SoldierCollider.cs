using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CircleCollider2D))]
public class SoldierCollider : KennMonoBehaviour
{
    [SerializeField] protected Soldier soldier;

    protected override void LoadComponent()
    {
        base.LoadComponent();
        soldier = GetComponent<Soldier>();
    }

    protected void OnTriggerEnter2D(Collider2D other)
    {
        if (other.TryGetComponent<Item>(out Item item))
        {
            soldier.inventory.AddItem(item, 1);
            item.gameObject.SetActive(false);
        }
        if (other.TryGetComponent<Weapon>(out Weapon weapon))
        {
            soldier.inventory.AddWeapon(weapon, 1);
            weapon.gameObject.SetActive(false);
        }    
    }
}
