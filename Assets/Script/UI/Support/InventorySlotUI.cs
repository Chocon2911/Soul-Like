using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventorySlotUI : HuyMonoBehaviour
{
    [SerializeField] private Transform selectedBg;
    [SerializeField] private Transform hoveredBg;
    [SerializeField] private InventoryItemUI item;

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
}
