using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : HuyMonoBehaviour
{
    //==========================================Variable==========================================
    [SerializeField] protected List<InventoryItem> inventoryItems = new List<InventoryItem>();
    [SerializeField] protected int maxCap;

    //==========================================Get Set===========================================
    public List<InventoryItem> InventoryItems => inventoryItems;
    public int MaxCap => maxCap;

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
        InventoryItem leftItem = item;

        foreach (InventoryItem inventoryItem in this.inventoryItems)
        {
            if (inventoryItem.itemName == item.itemName && inventoryItem.currAmount < inventoryItem.maxAmount)
            {
                inventoryItem.currAmount += leftItem.currAmount;
                if (inventoryItem.currAmount > inventoryItem.maxAmount)
                {
                    leftItem.currAmount = inventoryItem.currAmount - inventoryItem.maxAmount;
                    inventoryItem.currAmount = inventoryItem.maxAmount;
                }
                else return null;
            }
        }

        if (this.inventoryItems.Count < this.maxCap)
        {
            this.inventoryItems.Add(leftItem);
            return null;
        }

        return leftItem;
    }

    public void DropItem(InventoryItem item) 
    {
        foreach (InventoryItem inventoryItem in this.inventoryItems)
        {
            if (inventoryItem != item) continue;
            this.inventoryItems.Remove(inventoryItem);
            Transform droppedItem = DroppedItemSpawner.Instance.SpawnByName(item.itemName, transform.position, transform.rotation);
            droppedItem.GetComponent<DroppedItem>().Item = inventoryItem;
            droppedItem.gameObject.SetActive(true);
            break;
        }

        Debug.LogError("Drop item fail", gameObject);
    }
}
