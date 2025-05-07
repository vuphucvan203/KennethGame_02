using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ItemSlot : KennMonoBehaviour
{
    [SerializeField] protected Category currentCategory;
    [SerializeField] protected Item currentItem;
    [SerializeField] protected Image icon;
    [SerializeField] protected TextMeshProUGUI amount;
    [SerializeField] protected Button useButton;

    protected void Update()
    {
        if (!IsEmpty())
        {
            useButton.transform.gameObject.SetActive(true);
        }
        else useButton.transform.gameObject.SetActive(false);
    }

    protected override void LoadComponent()
    {
        base.LoadComponent();
        icon = transform.Find("Icon").GetComponent<Image>();
        amount = GetComponentInChildren<TextMeshProUGUI>();
        useButton = GetComponentInChildren<Button>();
    }

    public bool IsEmpty() => currentItem == null;

    public void SetItem(Category category)
    {
        if (category != null)
        {
            Debug.Log("Set item: " + category.item.name);
            currentCategory = category;
            currentItem = category.item;
            icon.transform.gameObject.SetActive(true);
            icon.sprite = category.item.Sprite;
            icon.SetNativeSize();
            this.amount.text = category.amount > 0 ? category.amount.ToString() : string.Empty;
        }
        else
        {
            currentCategory = null;
            currentItem = null;
            icon.transform.gameObject.SetActive(false);
            icon.sprite = null;
            this.amount.text = string.Empty;
        } 
    }

    public void UseItem()
    {
        currentItem.UseItem(Player.Instance.controller.Soldier);
        List<Category> categories = Player.Instance.controller.Soldier.inventory.ItemForUse;
        Category category = categories.Find(x => x == currentCategory);
        category.amount--;
        if (category.amount <= 0)
        {
            currentItem = null;
            categories.Remove(category);
            SetItem(null);
        }
    }
}
