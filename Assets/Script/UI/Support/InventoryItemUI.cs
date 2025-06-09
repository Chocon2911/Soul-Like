using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InventoryItemUI : HuyMonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    //==========================================Variable==========================================
    public InventorySlotUI currSlot;
    private InventoryUI inventoryUI;
    private RectTransform rectTransform;
    private Image icon;
    private InventoryItems item;
    private string inventorySlotTag;

    public InventoryItems Item 
    {
        get
        {
            return item;
        }
        set
        {
            this.icon.sprite = value.icon;
            item = value;
        }
    }

    //===========================================Unity============================================
    public override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadComponent(ref this.rectTransform, transform, "LoadRectTransform()");
        this.LoadComponent(ref this.inventoryUI, transform.parent.parent, "LoadInventoryUI()");
    }

    //=========================================Interface==========================================
    public void OnBeginDrag(PointerEventData eventData)
    {
        this.currSlot = transform.parent.GetComponent<InventorySlotUI>();
        this.transform.SetParent(this.inventoryUI.transform);
    }

    public void OnDrag(PointerEventData eventData)
    {
        this.rectTransform.anchoredPosition += eventData.delta / this.inventoryUI.Canvas.scaleFactor;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        GameObject target = eventData.pointerEnter;

        if (target != null && target.CompareTag(this.inventorySlotTag))
        {
            transform.SetParent(target.transform);
            this.currSlot = target.GetComponent<InventorySlotUI>();
            this.rectTransform.anchoredPosition = Vector2.zero;
        }
        else
        {
            transform.SetParent(this.currSlot.transform);
            this.rectTransform.anchoredPosition = Vector2.zero;
        }
    }
}
