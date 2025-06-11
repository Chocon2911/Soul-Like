using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class InventoryUI : HuyMonoBehaviour
{
    //==========================================Variable==========================================
    private static InventoryUI instance;
    public static InventoryUI Instance => instance;

    [SerializeField] private Transform content;
    [SerializeField] private Transform container;
    [SerializeField] private List<InventoryItemUI> items;
    [SerializeField] private List<InventorySlotUI> slots;
    [SerializeField] private Transform dragArea;
    [SerializeField] private InventoryItemUI carriedItem;
    [SerializeField] private string inventorySlotName;

    //==========================================Get Set===========================================
    public Transform DragArea => dragArea;
    public InventoryItemUI CarriedItem { get => this.carriedItem; set => this.carriedItem = value; }

    //===========================================Unity============================================
    public override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadComponent(ref this.content, transform.Find("Content"), "LoadContent()");
        this.LoadComponent(ref this.container, this.content.Find("Container"), "LoadGrid()");
        this.LoadComponent(ref this.slots, this.content.Find("Container"), "LoadSlots()");
        this.LoadComponent(ref this.dragArea, this.content.Find("DragArea"), "LoadDragArea()");
    }

    protected override void Awake()
    {
        if (instance != null)
        {
            Debug.LogError("instance not null (transform)", transform.gameObject);
            Debug.LogError("Instance not null (instance)", instance.gameObject);
            return;
        }

        instance = this;
        base.Awake();
    }

    private void Update()
    {
        this.Displaying();
    }

    private void FixedUpdate()
    {
        this.ItemFollowingMouse();
    }



    //============================================================================================
    //===========================================Method===========================================
    //============================================================================================

    //==========================================Display===========================================
    private void Displaying()
    {
        if (Input.GetKeyDown(KeyCode.B))
        {
            if (this.content.gameObject.activeSelf)
            {
                this.content.gameObject.SetActive(false);
            }
            else
            {
                this.Refresh();
                this.content.gameObject.SetActive(true);
            }
        }
    }

    //==========================================Refresh===========================================
    public void Refresh()
    {
        Inventory playerInventory = GameManager.Instance.Player.Inventory;
        int cap = playerInventory.MaxCap;
        this.UpdateSlots(cap);
        List<InventoryItem> leftItems = UpdateItemInfo(playerInventory.InventoryItems);
        this.AddItemsToEmptySlots(leftItems);
    }
    
    private void UpdateSlots(int cap) 
    {
        int newSlotAmount = cap - this.slots.Count;
        if (newSlotAmount <= 0) return;

        for (int i = 0; i < newSlotAmount; i++)
        {
            Transform newSlot = UISpawner.Instance.SpawnByCode(UISpawnCode.INVENTORY_SLOT, Vector3.zero, Quaternion.identity);
            RectTransform rectTransform = newSlot.GetComponent<RectTransform>();
            rectTransform.SafeSetParent(this.container.transform);
            newSlot.transform.SetSiblingIndex(this.slots.Count);
            newSlot.gameObject.SetActive(true);

            InventorySlotUI slot = newSlot.GetComponent<InventorySlotUI>();
            slot.Index = slot.transform.GetSiblingIndex();
            this.slots.Add(slot);
        }
    }

    private List<InventoryItem> UpdateItemInfo(List<InventoryItem> inventoryItems)
    {
        List<InventoryItem> leftItems = inventoryItems.ToList();

        foreach (InventoryItem item in inventoryItems)
        {
            foreach (InventorySlotUI slot in this.slots)
            {
                if (slot.ItemUI == null || slot.ItemUI.Item == null || slot.ItemUI.Item != item) continue;
                slot.ItemUI.Item = item;
                leftItems.Remove(item);
            }
        }

        return leftItems;
    }

    private void AddItemsToEmptySlots(List<InventoryItem> inventoryItems)
    {
        List<InventoryItem> leftItems = inventoryItems.ToList();
        foreach (InventoryItem leftItem in inventoryItems)
        {
            foreach (InventorySlotUI slot in this.slots)
            {
                if (slot.ItemUI != null) continue;
                Transform newItemUI = UISpawner.Instance.SpawnByCode(UISpawnCode.INVENTORY_ITEM, Vector3.zero, Quaternion.identity);
                newItemUI.gameObject.SetActive(true);
                InventoryItemUI itemUI = newItemUI.GetComponent<InventoryItemUI>();
                if (itemUI == null) Debug.Log("Fuck");
                slot.ItemUI = itemUI;
                itemUI.CurrSlot = slot;

                itemUI.Default(leftItem, this.dragArea);
                leftItems.Remove(leftItem);
                break;
            }
        }

        if (leftItems.Count <= 0) return;
        Debug.LogError("AddItemsToEmptySlots: " + leftItems.Count + " items left", gameObject);
    }

    //=====================================Item Follow Mouse======================================
    private void ItemFollowingMouse()
    {
        if (this.carriedItem == null) return;
        this.carriedItem.transform.position = InputManager.Instance.MousePos;
    }

    //===========================================Other============================================
    private void Default()
    {
        foreach (InventorySlotUI slot in this.slots)
        {
            slot.Index = slot.transform.GetSiblingIndex();
        }
    }
}