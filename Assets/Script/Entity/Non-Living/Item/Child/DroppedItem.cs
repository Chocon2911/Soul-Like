using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CircleCollider2D))]
public class DroppedItem : Item
{
    //==========================================Variable==========================================
    [Header("===Dropped Item===")]
    [SerializeField] protected CircleCollider2D bodyCol;
    [SerializeField] protected Transform target;
    [SerializeField] protected float moveSpeed;

    //===========================================Unity============================================
    public override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadComponent(ref this.bodyCol, transform, "LoadBodyCol()");
    }

    protected virtual void Update()
    {
        this.Moving();
    }

    public virtual void SetTarget(Transform target)
    {
        this.target = target;
    }

    //===========================================Method===========================================
    protected virtual void Moving()
    {
        if (this.target == null) return;
        transform.position = Vector3.MoveTowards(transform.position, this.target.position, this.moveSpeed * Time.deltaTime);
    }
}
