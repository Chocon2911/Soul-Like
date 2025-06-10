using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InventoryItemUI : HuyMonoBehaviour, IPointerClickHandler, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    //==========================================Variable==========================================
    public InventorySlotUI currSlot;
    private InventoryUI inventoryUI;
    private RectTransform rectTransform;
    private Image icon;
    private InventoryItem item;
    private Transform dragArea;
    private string inventorySlotTag;

    //==========================================Get Set===========================================
    public InventoryItem Item 
    {
        get
        {
            return this.item;
        }
        set
        {
            this.icon.sprite = value.icon;
            this.item = value;
        }
    }

    //===========================================Unity============================================
    public override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadComponent(ref this.rectTransform, transform, "LoadRectTransform()");
        this.LoadComponent(ref this.inventoryUI, transform.parent.parent, "LoadInventoryUI()");
        this.LoadComponent(ref this.icon, transform, "LoadIcon()");
    }

    public void Default(InventoryItem inventoryItem, Transform dragArea)
    {
        this.item = inventoryItem;
        this.dragArea = dragArea;
        this.icon.sprite = inventoryItem.icon;
    }

    //=========================================Interface==========================================
    void IPointerClickHandler.OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Left)
        {
            InventoryUI.Instance.CarriedInventoryItemUI = this;
            transform.SetParent(this.dragArea);
        }
        else if (eventData.button == PointerEventData.InputButton.Right)
        {
            //TODO: Display item info
            Debug.Log("Right Click on Item", gameObject);
        }
    }

    void IBeginDragHandler.OnBeginDrag(PointerEventData eventData)
    {
        this.currSlot = transform.parent.GetComponent<InventorySlotUI>();
        this.transform.SetParent(this.inventoryUI.transform);
    }

    void IDragHandler.OnDrag(PointerEventData eventData)
    {
        this.rectTransform.anchoredPosition += eventData.delta / this.inventoryUI.Canvas.scaleFactor;
    }

    void IEndDragHandler.OnEndDrag(PointerEventData eventData)
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
