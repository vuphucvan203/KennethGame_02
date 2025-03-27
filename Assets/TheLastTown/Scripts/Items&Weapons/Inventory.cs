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
    [SerializeField] protected List<Category> itemForUse;
    [SerializeField] protected List<Weapon> weaponOwner;

    public void AddItem(Item item, int amount)
    {
        Category category = new Category(item, amount);
        itemForUse.Add(category);
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
    }

    public void DropWeapon(Weapon weapon, int amount)
    {
        foreach(Weapon wea in weaponOwner)
        {
            if(weapon == wea) weaponOwner.Remove(weapon);
        }    
    }
}
