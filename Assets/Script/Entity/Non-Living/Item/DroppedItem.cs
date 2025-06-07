using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CircleCollider2D), typeof(Rigidbody2D))]
public class DroppedItem : HuyMonoBehaviour
{
    //==========================================Variable==========================================
    [Header("===Dropped Item===")]
    [SerializeField] protected InventoryItem item;
    [SerializeField] protected Rigidbody2D rb;
    [SerializeField] protected CircleCollider2D bodyCol;
    [SerializeField] protected float moveSpeed;

    //==========================================Get Set===========================================
    public InventoryItem Item { get => item; set => item = value; }

    //===========================================Unity============================================
    public override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadComponent(ref this.rb, transform, "LoadRb()");
        this.LoadComponent(ref this.bodyCol, transform, "LoadBodyCol()");
    }

    //===========================================Method===========================================
    public virtual void Moving(Transform target)
    {
        if (target == null) return;
        Vector2 newPos = Vector2.Lerp(transform.position, target.position, this.moveSpeed * Time.deltaTime);
        this.rb.MovePosition(newPos);
    }

    public virtual void CopyStat(InventoryItem newItem)
    {
        this.item = newItem;
    }
}
