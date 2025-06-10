using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item
{
    [Header("Item")]
    public string id;
    public string itemName;
    public Sprite icon;
    public ItemType itemType;
    public int maxAmount;
    public int currAmount;

    public Item(string id, string itemName, Sprite icon, ItemType itemType, int maxAmount, int currAmount)
    {
        this.id = id;
        this.itemName = itemName;
        this.icon = icon;
        this.itemType = itemType;
        this.maxAmount = maxAmount;
        this.currAmount = currAmount;
    }

    public Item() { }
}
