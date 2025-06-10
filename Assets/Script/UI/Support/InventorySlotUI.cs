using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class InventorySlotUI : HuyMonoBehaviour, IPointerClickHandler
{
    //==========================================Variable==========================================
    [SerializeField] private Transform selectedBg;
    [SerializeField] private Transform hoveredBg;
    [SerializeField] private InventoryItemUI item;
    [SerializeField] private string inventoryItemUIName;

    public InventoryItemUI Item => item;

    //===========================================Unity============================================
    public override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadComponent(ref this.selectedBg, transform.Find("SelectedImg"), "LoadSelectedBg()");
        this.LoadComponent(ref this.hoveredBg, transform.Find("HoveredImg"), "LoadHoveredBg()");
    }

    private void FixedUpdate()
    {
        this.selectedBg.gameObject.SetActive(false);
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

    public void CreateAndSetInventoryItem(InventoryItem inventoryItem, Transform dragArea)
    {
        if (inventoryItem == null)
        {
            UISpawner.Instance.Despawn(this.item.transform);
            this.item = null;
        }

        else
        {
            Transform newInventoryItemUI = UISpawner.Instance.SpawnByName(this.inventoryItemUIName, Vector3.zero, Quaternion.identity);
            newInventoryItemUI.SetParent(transform, false);
            this.item = newInventoryItemUI.GetComponent<InventoryItemUI>();
            this.item.Default(inventoryItem, dragArea);
            this.item.gameObject.SetActive(true);
        }
    }

    private void SetInventoryItemUI(InventoryItemUI inventoryItemUI)
    {
        inventoryItemUI.transform.SetParent(transform, false);
        this.item = inventoryItemUI.GetComponent<InventoryItemUI>();
    }

    //=========================================Interface==========================================
    void IPointerClickHandler.OnPointerClick(PointerEventData eventData)
    {
        InventoryUI inventoryUI = InventoryUI.Instance;
        if (eventData.button == PointerEventData.InputButton.Left && !inventoryUI.CarriedInventoryItemUI)
        {
            this.SetInventoryItemUI(inventoryUI.CarriedInventoryItemUI);
        }
    }
}
