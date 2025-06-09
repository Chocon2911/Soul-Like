using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryUI : HuyMonoBehaviour
{
    //==========================================Variable==========================================
    private static InventoryUI instance;
    public static InventoryUI Instance => instance;

    [SerializeField] private GridLayout grid;
    [SerializeField] private List<InventoryItemUI> items;
    [SerializeField] private List<InventorySlotUI> slots;
    [SerializeField] private Canvas canvas;
    [SerializeField] private string inventorySlotName;

    //==========================================Get Set===========================================
    public Canvas Canvas => this.canvas;

    //===========================================Unity============================================
    public override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadComponent(ref this.canvas, transform, "LoadCanvas()");
        this.LoadChildComponent(ref this.slots, transform.Find("Container"), "LoadSlots()");
        this.LoadComponent(ref this.grid, transform.Find("Container"), "LoadGrid()");
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

    //===========================================Method===========================================
    public virtual void Show()
    {
        foreach (InventorySlotUI slot in this.slots)
        {

        }
    }
}