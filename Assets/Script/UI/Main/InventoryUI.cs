using System.Collections;
using System.Collections.Generic;
using Unity.IO.LowLevel.Unsafe;
using Unity.VisualScripting;
using UnityEngine;

public class InventoryUI : HuyMonoBehaviour
{
    //==========================================Variable==========================================
    private static InventoryUI instance;
    public static InventoryUI Instance => instance;

    [SerializeField] private Transform content;
    [SerializeField] private GridLayout grid;
    [SerializeField] private List<InventoryItemUI> items;
    [SerializeField] private List<InventorySlotUI> slots;
    [SerializeField] private Canvas canvas;
    [SerializeField] private InventoryItemUI carriedInventoryItemUI;
    [SerializeField] private Transform dragArea;
    [SerializeField] private string inventorySlotName;

    //==========================================Get Set===========================================
    public Canvas Canvas => this.canvas;
    public InventoryItemUI CarriedInventoryItemUI
    {
        get => this.carriedInventoryItemUI;
        set => this.carriedInventoryItemUI = value;
    }

    public Transform DragArea => dragArea;

    //===========================================Unity============================================
    public override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadComponent(ref this.content, transform.Find("Content"), "LoadContent()");
        this.LoadComponent(ref this.grid, transform.Find("Content").Find("Container"), "LoadGrid()");
        this.LoadComponent(ref this.slots, transform.Find("Content").Find("Container"), "LoadSlots()");
        this.LoadComponent(ref this.canvas, transform, "LoadCanvas()");
        this.LoadComponent(ref this.dragArea, transform.Find("Content").Find("DragArea"), "LoadDragArea()");
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

    private void FixedUpdate()
    {
        this.CarriedItemFollowMouse();
    }

    //===========================================Method===========================================
    public void Show()
    {
        this.UpdateUI();
        this.content.gameObject.SetActive(true);
    }

    public void Hide()
    {
        this.content.gameObject.SetActive(false);
    }

    public virtual void UpdateUI()
    {
        Inventory playerInventory = GameManager.Instance.Player.Inventory;
        int newSlotAmount = playerInventory.MaxCap - this.slots.Count;
        if (newSlotAmount > 0)
        {
            for (int i = 0; i < newSlotAmount; i++) this.AddNewSlot();
        }

        this.ClearInventorySlots();
        foreach (InventoryItem inventoryItem in playerInventory.InventoryItems)
        {
            if (inventoryItem.slotIndex == -1) continue; // Item on dragging
            this.slots[inventoryItem.slotIndex].CreateAndSetInventoryItem(inventoryItem, this.dragArea);
        }
    }
    
    public int GetFirstEmptySlotIndex()
    {
        int index = 0;
        foreach (InventorySlotUI slot in this.slots)
        {
            if (slot.Item == null) return index;
            index++;
        }

        Debug.LogError("No empty slot", gameObject);
        return -1; // No empty slot
    }

    private void AddNewSlot()
    {
        Transform newSlot = UISpawner.Instance.SpawnByName(this.inventorySlotName, Vector3.zero, Quaternion.identity);
        newSlot.SetParent(this.grid.transform, false);
        newSlot.gameObject.SetActive(true);
        this.slots.Add(newSlot.GetComponent<InventorySlotUI>());
    }

    private void ClearInventorySlots()
    {
        foreach (InventorySlotUI slot in this.slots)
        {
            slot.CreateAndSetInventoryItem(null, null);
        }
    }

    private void CarriedItemFollowMouse()
    {
        if (this.carriedInventoryItemUI == null) return;
        this.carriedInventoryItemUI.transform.position = InputManager.Instance.MousePos;
    }
}