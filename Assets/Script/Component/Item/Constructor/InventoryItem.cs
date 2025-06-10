using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class InventoryItem : Item
{
    [Header("===Inventory Item===")]
    public int slotIndex;

    public InventoryItem(string id, string itemName, Sprite icon, ItemType itemType, 
        int maxAmount, int currAmount, int slotIndex) : 
        base(id, itemName, icon, itemType, maxAmount, currAmount)
    {
        this.slotIndex = slotIndex;
    }

    public InventoryItem(Item item, int slotIndex) : base()
    {
        this.id = item.id;
        this.itemName = item.itemName;
        this.icon = item.icon;
        this.itemType = item.itemType;
        this.maxAmount = item.maxAmount;
        this.currAmount = item.currAmount;
        this.slotIndex = slotIndex;
    }

    public InventoryItem() : base() { }
}
