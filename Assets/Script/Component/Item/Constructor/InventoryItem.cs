using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class InventoryItem
{
    [Header("===Inventory Item===")]
    public string id;
    public string itemName;
    public Sprite icon;
    public ItemType itemType;
    public int maxAmount;
    public int currAmount;
}
