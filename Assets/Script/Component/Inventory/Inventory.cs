using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : HuyMonoBehaviour
{
    //==========================================Variable==========================================
    [SerializeField] protected List<InventoryItem> inventoryItems = new List<InventoryItem>();
    [SerializeField] protected int maxCap;

    //===========================================Unity============================================
    public override void LoadComponents()
    {
        base.LoadComponents();
    }

    //===========================================Method===========================================
    public InventoryItem AddItem(DroppedItem item)
    {
        return this.AddItem(item.Item);
    }

    public InventoryItem AddItem(InventoryItem item)
    {
        int amountLeft = item.currAmount;

        foreach (InventoryItem inventoryItem in this.inventoryItems)
        {
            if (inventoryItem.itemName == item.itemName && inventoryItem.currAmount < inventoryItem.maxAmount)
            {
                inventoryItem.currAmount += amountLeft;
                if (inventoryItem.currAmount > inventoryItem.maxAmount)
                {
                    amountLeft = inventoryItem.maxAmount - inventoryItem.currAmount;
                    inventoryItem.currAmount = inventoryItem.maxAmount;
                }
            }
        }

        if (this.inventoryItems.Count < maxCap)
        {
            this.inventoryItems.Add(item);
            return null;
        }

        return item;
    }
}
