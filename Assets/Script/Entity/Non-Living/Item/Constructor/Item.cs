using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : HuyMonoBehaviour
{
    [SerializeField] protected string itemName;
    [SerializeField] protected ItemType itemType;
    [SerializeField] protected int maxAmount;
    [SerializeField] protected int currAmount;

    public string ItemName { get => itemName; set => itemName = value; }
    public int MaxAmount { get => maxAmount; set => maxAmount = value; }
    public int CurrAmount { get => currAmount; set => currAmount = value; }
}
