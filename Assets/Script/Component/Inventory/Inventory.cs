using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : HuyMonoBehaviour
{
    //==========================================Variable==========================================
    [SerializeField] protected Transform container;
    [SerializeField] protected List<InventoryItem> inventoryItems;
    [SerializeField] protected int maxCap;

    public override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadComponent(ref this.container, transform.Find("Container"), "LoadContainer()");
    }

    public InventoryItem AddItem(InventoryItem item)
    {
        int amountLeft = item.CurrAmount;

        foreach (InventoryItem inventoryItem in this.inventoryItems)
        {
            if (inventoryItem.ItemName == item.ItemName && inventoryItem.CurrAmount < inventoryItem.MaxAmount)
            {
                inventoryItem.CurrAmount += amountLeft;
                if (inventoryItem.CurrAmount > inventoryItem.MaxAmount)
                {
                    amountLeft = inventoryItem.MaxAmount - inventoryItem.CurrAmount;
                    inventoryItem.CurrAmount = inventoryItem.MaxAmount;
                }
            }
        }

        if (this.inventoryItems.Count < maxCap)
        {
            this.inventoryItems.Add(item);
            item.transform.parent = this.container;
        }

        return null;
    }
}
