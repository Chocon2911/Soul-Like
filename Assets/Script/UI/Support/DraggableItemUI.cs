using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DraggableItemUI : HuyMonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    //==========================================Variable==========================================
    public Transform currSlot;
    private InventoryUI inventoryUI;
    private CanvasGroup canvasGroup;
    private RectTransform rectTransform;
    private Image icon;
    private InventoryItem item;
    private string inventorySlotTag;

    public InventoryItem Item 
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
        this.LoadComponent(ref this.canvasGroup, transform, "LoadCanvasGroup()");
        this.LoadComponent(ref this.inventoryUI, transform.parent.parent, "LoadInventoryUI()");
    }

    //=========================================Interface==========================================
    public void OnBeginDrag(PointerEventData eventData)
    {
        this.currSlot = transform.parent;
        this.transform.SetParent(this.inventoryUI.transform);
        this.canvasGroup.blocksRaycasts = false;
    }

    public void OnDrag(PointerEventData eventData)
    {
        this.rectTransform.anchoredPosition += eventData.delta / this.inventoryUI.Canvas.scaleFactor;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        this.canvasGroup.blocksRaycasts = true;
        GameObject target = eventData.pointerEnter;

        if (target != null && target.CompareTag(this.inventorySlotTag))
        {
            transform.SetParent(target.transform);
            this.rectTransform.anchoredPosition = Vector2.zero;
        }
        else
        {
            transform.SetParent(this.currSlot);
            this.rectTransform.anchoredPosition = Vector2.zero;
        }
    }
}
