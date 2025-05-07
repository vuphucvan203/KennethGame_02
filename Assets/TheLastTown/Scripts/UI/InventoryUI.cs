using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class InventoryUI : KennMonoBehaviour
{
    [SerializeField] protected List<ItemSlot> itemSlots;
    [SerializeField] protected List<Category> categories;
    [SerializeField] protected Transform panel;

    protected override void Start()
    {
        base.Start();
        panel.gameObject.SetActive(false);
    }

    protected void Update()
    {
        categories = Player.Instance.controller.Soldier.inventory.ItemForUse;

        if (categories.Count > 0)
        {
            for (int i = 0; i < itemSlots.Count; i++)
            {
                if (i < categories.Count)
                {
                    itemSlots[i].SetItem(categories[i]);
                }
                else
                {
                    itemSlots[i].SetItem(null);
                }
            }
        }
        else
        {
            for (int i = 0; i < itemSlots.Count; i++)
            {
                itemSlots[i].SetItem(null);
            }
        }
    }

    protected override void LoadComponent()
    {
        base.LoadComponent();
        panel = transform.Find("Panel").GetComponent<Transform>();
        if (itemSlots.Count > 0) return;
        itemSlots = GetComponentsInChildren<ItemSlot>().ToList();
    }

    public void OpenInventoryPane()
    {
        panel.transform.gameObject.SetActive(true);
    }

    public void CloseInventoryPane()
    {
        panel.transform.gameObject.SetActive(false);
    }    
}
