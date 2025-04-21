using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[Serializable]
public class  Category
{
    public Item item;
    public int amount;

    public Category(Item item, int amount)
    {
        this.item = item;
        this.amount = amount;
    }
}

public class Inventory : KennMonoBehaviour
{
    [SerializeField] protected Soldier soldier;
    [SerializeField] protected List<Category> itemForUse;
    [SerializeField] protected List<Weapon> weaponOwner;
    [SerializeField] protected LayerMask layerMask;

    protected void Update()
    {
        InventoryActive();
    }

    protected override void LoadComponent()
    {
        base.LoadComponent();
        soldier = GetComponent<Soldier>();
    }

    protected void InventoryActive()
    {
        Collider2D rangePickupable = Physics2D.OverlapCircle(transform.position, 3f, layerMask);
        if(rangePickupable != null)
        {
            if(rangePickupable.TryGetComponent<Item>(out Item item)) AddItem(item, 1);
            if (rangePickupable.TryGetComponent<Weapon>(out Weapon weapon)) AddWeapon(weapon, 1);
        } 
    }    

    public void AddItem(Item item, int amount)
    {
        if (!item.isPickupable) return;
        Category category = new Category(item, amount);
        itemForUse.Add(category);
        item.UseItem(soldier);
        item.transform.gameObject.SetActive(false);
    }   
    
    public void DropItem(Item item, int amount)
    {
        foreach(Category category in itemForUse)
        {
            if(category.item == item)
            {
                category.amount -= amount;
                if(category.amount <= 0) itemForUse.Remove(category);
            }    
        }
    }

    public void AddWeapon(Weapon weapon, int amount)
    {
        weaponOwner.Add(weapon);
        weapon.transform.gameObject.SetActive(false);
    }

    public void DropWeapon(Weapon weapon, int amount)
    {
        foreach(Weapon wea in weaponOwner)
        {
            if(weapon == wea) weaponOwner.Remove(weapon);
        }    
    }
}
