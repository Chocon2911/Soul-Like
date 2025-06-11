using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class InventoryItem
{
    [Header("InventoryItem")]
    public string id;
    public string itemName;
    public Sprite icon;
    public ItemType itemType;
    public int maxAmount;
    public int currAmount;

    public InventoryItem(string id, string itemName, Sprite icon, ItemType itemType, int maxAmount, int currAmount)
    {
        this.id = id;
        this.itemName = itemName;
        this.icon = icon;
        this.itemType = itemType;
        this.maxAmount = maxAmount;
        this.currAmount = currAmount;
    }

    public InventoryItem() { }
}
