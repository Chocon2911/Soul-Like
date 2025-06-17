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

    //==========================================Get Set===========================================
    public InventorySlotUI CurrSlot
    {
        get => this.currSlot;
        set => this.currSlot = value;
    }

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
        this.LoadComponent(ref this.icon, transform, "LoadIcon()");
    }

    //===========================================Method===========================================
    public void Default(InventoryItem inventoryItem)
    {
        this.Item = inventoryItem;
    }

    //=========================================Interface==========================================
    void IPointerClickHandler.OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Left)
        {
            InventoryUI inventoryUI = InventoryUI.Instance;
            if (inventoryUI.CarriedItem != null) // Currently holding item
            {
                List<RaycastResult> results = new List<RaycastResult>();
                EventSystem.current.RaycastAll(eventData, results);
                foreach (RaycastResult result in results)
                {
                    InventorySlotUI slot = result.gameObject.GetComponent<InventorySlotUI>();
                    if (slot == null) continue;
                    else if (slot.ItemUI == null)
                    {
                        inventoryUI.AttachItemToSlot(this, slot);
                        inventoryUI.CarriedItem = null;
                    }
                    else
                    {
                        slot.SwapWithCurrItem(inventoryUI.CarriedItem);
                    }
                    break;
                }
            }
            else // Currently not holding item
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
