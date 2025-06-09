using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventorys : HuyMonoBehaviour
{
    //==========================================Variable==========================================
    [SerializeField] protected List<InventoryItems> inventoryItems = new List<InventoryItems>();
    [SerializeField] protected int maxCap;

    //===========================================Unity============================================
    public override void LoadComponents()
    {
        base.LoadComponents();
    }

    //===========================================Method===========================================
    public InventoryItems AddItem(DroppedItem item)
    {
        return this.AddItem(item.Item);
    }

    public InventoryItems AddItem(InventoryItems item)
    {
        int amountLeft = item.currAmount;

        foreach (InventoryItems inventoryItem in this.inventoryItems)
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

    public void DropItem(InventoryItems item) 
    {
        
    }
}
