using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InventoryItemUI : HuyMonoBehaviour, IPointerClickHandler
{
    //==========================================Variable==========================================
    [SerializeField] private InventorySlotUI currSlot;
    [SerializeField] private RectTransform rectTransform;
    [SerializeField] private Image icon;
    [SerializeField] private InventoryItem item;
    [SerializeField] private Transform dragArea;

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

    public InventorySlotUI CurrSlot { get => currSlot; set => currSlot = value; }

    //===========================================Unity============================================
    public override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadComponent(ref this.rectTransform, transform, "LoadRectTransform()");
        this.LoadComponent(ref this.icon, transform, "LoadIcon()");
    }

    //===========================================Method===========================================
    public void Default(InventoryItem inventoryItem, Transform dragArea)
    {
        this.Item = inventoryItem;
        this.dragArea = dragArea;
    }

    //=========================================Interface==========================================
    void IPointerClickHandler.OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Left)
        {
            transform.SetParent(this.dragArea);
            InventoryUI inventoryUI = InventoryUI.Instance;
            if (inventoryUI.CarriedItem != null)
            {
                List<RaycastResult> results = new List<RaycastResult>();
                EventSystem.current.RaycastAll(eventData, results);
                foreach (RaycastResult result in results)
                {
                    InventorySlotUI inventorySlotUI = result.gameObject.GetComponent<InventorySlotUI>();
                    if (inventorySlotUI == null) continue;
                    else if (inventorySlotUI.ItemUI == null) inventorySlotUI.ItemUI = this;
                    else inventorySlotUI.SwapItemWithCurrInSlot(this, inventoryUI.CarriedItem);
                    inventoryUI.CarriedItem = null;
                    break;
                }
            }
            else
            {
                inventoryUI.CarriedItem = this;
                this.currSlot.ItemUI = null;
                this.currSlot = null;
            }
        }
        else if (eventData.button == PointerEventData.InputButton.Right)
        {
            //TODO: Display item info
            Debug.Log("Right Click on Item", gameObject);
        }
    }
}
