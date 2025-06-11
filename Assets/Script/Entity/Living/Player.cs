using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D), typeof(CapsuleCollider2D))]
public class Player : HuyMonoBehaviour
{
    //==========================================Variable==========================================
    [Header("Component")]
    [SerializeField] protected Rigidbody2D rb;
    [SerializeField] protected Inventory inventory;

    [Header("Move")]
    [SerializeField] protected float slowDownTime;
    [SerializeField] protected float speedUpTime;
    [SerializeField] protected float moveSpeed;

    [Header("Item Detection")]
    [SerializeField] protected CircleCollider2D itemDetectCol;
    [SerializeField] protected LayerMask itemLayer;
    [SerializeField] protected LayerMask wallLayer;
    [SerializeField] protected List<string> itemTags = new List<string>();

    //==========================================Get Set===========================================
    //===Component===
    public Inventory Inventory => this.inventory;

    //===========================================Unity============================================
    public override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadComponent(ref this.rb, transform, "LoadRb()");
        this.LoadComponent(ref this.itemDetectCol, transform.Find("ItemDetect"), "LoadItemDetectCol()");
        this.LoadComponent(ref this.inventory, transform.Find("Inventory"), "LoadInventory()");
    }

    protected virtual void OnTriggerEnter2D(Collider2D collision)
    {
        this.CollideWithItem(collision);
    }

    protected virtual void Update()
    {
        this.Moving();
        this.DetectingItem();
    }



    //============================================================================================
    //===========================================Method===========================================
    //============================================================================================

    //============================================Move============================================
    protected virtual void Moving()
    {
        Util.Instance.MovingWithAcceleration(this.rb, InputManager.Instance.MoveDir, this.moveSpeed, this.speedUpTime, this.slowDownTime);
    }

    //=======================================Item Detection=======================================
    protected virtual void DetectingItem()
    {
        List<DroppedItem> items = this.GetItemsInRange();

        if (items.Count <= 0) return;
        items = this.GetValidItemsByRayCast(items);
        this.MovingItems(items);
        
    }

    protected virtual List<DroppedItem> GetItemsInRange()
    {
        Vector2 point = this.itemDetectCol.transform.position;
        float rad = this.itemDetectCol.radius;
        Collider2D[] cols = Physics2D.OverlapCircleAll(point, rad, this.itemLayer);

        List<DroppedItem> items = new List<DroppedItem>();
        for (int i = 0; i < cols.Length; i++)
        {
            if (this.itemTags.Contains(cols[i].tag))
            {
                items.Add(cols[i].GetComponent<DroppedItem>());
            }
        }

        return items;
    }

    protected virtual List<DroppedItem> GetValidItemsByRayCast(List<DroppedItem> items)
    {
        Vector2 origin = this.itemDetectCol.transform.position;
        List<DroppedItem> validItems = new List<DroppedItem>();

        foreach (DroppedItem item in items)
        {
            if (item == null) continue;

            Vector2 itemPos = item.transform.position;
            Vector2 direction = itemPos - origin;
            float distance = direction.magnitude;

            RaycastHit2D hit = Physics2D.Raycast(origin, direction.normalized, distance, this.wallLayer);

            if (hit.collider == null) validItems.Add(item);

            #if UNITY_EDITOR
            Debug.DrawLine(origin, itemPos, hit.collider == null ? Color.green : Color.red, 0.5f);
            #endif
        }

        return validItems;
    }

    protected virtual void MovingItems(List<DroppedItem> items)
    {
        foreach (DroppedItem item in items) item.Moving(transform);
    }

    //=====================================Collide With Item======================================
    protected virtual void CollideWithItem(Collider2D collision)
    {
        if (!this.itemTags.Contains(collision.tag)) return;
        DroppedItem droppedItem = collision.GetComponent<DroppedItem>();
        InventoryItem leftItem = this.inventory.AddItem(droppedItem);

        if (leftItem == null) DroppedItemSpawner.Instance.Despawn(droppedItem.transform);
        else droppedItem.Item = leftItem;
    }
}
