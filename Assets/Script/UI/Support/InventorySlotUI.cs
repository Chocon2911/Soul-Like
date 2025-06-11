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
        get
        {
            return this.itemUI;
        }
        set
        {
            this.itemUI = value;
            if (value == null) return;
            value.transform.localPosition = Vector3.zero;
            value.transform.SafeSetParent(transform);
            value.CurrSlot = this;
        }
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

    public void SwapItemWithCurrInSlot(InventoryItemUI firstItemUI, InventoryItemUI secondItemUI)
    {
        if (firstItemUI == this.itemUI)
        {
            this.itemUI = secondItemUI;
            this.itemUI.GetComponent<RectTransform>().SafeSetParent(transform);
            this.itemUI.transform.position = Vector3.zero;
            InventoryUI.Instance.CarriedItem = secondItemUI;
        }
        else if (secondItemUI == this.itemUI)
        {
            this.itemUI = firstItemUI;
            this.itemUI.GetComponent<RectTransform>().SafeSetParent(transform);
            this.itemUI.transform.position = Vector3.zero;
            InventoryUI.Instance.CarriedItem = firstItemUI;
        }

        Debug.LogError("None of the itemUI is in this slot", gameObject);
    }
}
