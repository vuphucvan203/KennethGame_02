using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

[Serializable]
public class Category
{
    public Item item;
    public int amount;
    protected int maxStack = 9;

    public Category(Item item, int amount)
    {
        this.item = item;
        this.amount = amount;
    }

    public bool IsMaxStack => amount == maxStack;
}

public class Inventory : KennMonoBehaviour
{
    [SerializeField] protected Soldier soldier;
    [SerializeField] protected List<Category> itemForUse;
    public List<Category> ItemForUse => itemForUse;
    [SerializeField] protected bool sameKind;
    [SerializeField] protected List<Weapon> weaponOwner;
    public List<Weapon> WeaponOwner => weaponOwner;
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
        if (rangePickupable != null)
        {
            if(rangePickupable.TryGetComponent<Item>(out Item item)) AddItem(item, 1);
            if (rangePickupable.TryGetComponent<Weapon>(out Weapon weapon)) AddWeapon(weapon, 1);
        } 
    }    

    public void AddItem(Item item, int amount)
    {
        if (!item.isPickupable) return;
        if (item.Type == ItemType.Money)
        {
            item.UseItem(soldier);
            item.transform.gameObject.SetActive(false);
            return;
        } 
            
        if (itemForUse.Count == 0) sameKind = false;
        for(int i = 0; i < itemForUse.Count; i++)
        {
            if (itemForUse[i].item.Type == item.Type && !itemForUse[i].IsMaxStack)
            {
                itemForUse[i].amount += amount;
                item.transform.gameObject.SetActive(false);
                sameKind = true;
                break;
            }
            else sameKind = false;
        }
        if (!sameKind)
        {
            AddCategory(item, amount);
        }
    }   

    protected void AddCategory(Item item, int amount)
    {
        Category category = new Category(item, amount);
        itemForUse.Add(category);
        //item.UseItem(soldier);
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
