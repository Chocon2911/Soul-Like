using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class InventorySlotUI : HuyMonoBehaviour
{
    //==========================================Variable==========================================
    [SerializeField] private Transform selectedBg;
    [SerializeField] private Transform hoveredBg;
    [SerializeField] private InventoryItemUI itemUI;
    [SerializeField] private int index;

    //==========================================Get Set===========================================
    public InventoryItemUI ItemUI 
    {
        get => this.itemUI;
        set => this.itemUI = value;
    }
    public int Index { get => this.index; set => this.index = value; }

    //===========================================Unity============================================
    public override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadComponent(ref this.selectedBg, transform.Find("SelectedImg"), "LoadSelectedBg()");
        this.LoadComponent(ref this.hoveredBg, transform.Find("HoveredImg"), "LoadHoveredBg()");
    }

    //===========================================Method===========================================
    public void TurnOnSelected()
    {
        this.selectedBg.gameObject.SetActive(true);
    }

    public void TurnOffSeletected()
    {
        this.selectedBg.gameObject.SetActive(false);
    }

    public void TurnOnHovered()
    {
        this.hoveredBg.gameObject.SetActive(true);
    }

    public void TurnOffHovered()
    {
        this.hoveredBg.gameObject.SetActive(false);
    }

    public void SwapWithCurrItem(InventoryItemUI newItemUI)
    {
        Debug.Log("Hello");
        InventoryItemUI tempItemUI = this.itemUI;
        tempItemUI.CurrSlot = null;
        InventoryUI.Instance.CarriedItem = tempItemUI;
        InventoryUI.Instance.AttachItemToSlot(newItemUI, this);

        return;
    }
}
