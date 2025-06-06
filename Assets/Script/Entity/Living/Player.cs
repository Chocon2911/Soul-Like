using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : HuyMonoBehaviour
{
    //==========================================Variable==========================================
    [Header("Component")]
    [SerializeField] protected Rigidbody2D rb;

    [Header("Move")]
    [SerializeField] protected float slowDownTime;
    [SerializeField] protected float speedUpTime;
    [SerializeField] protected float moveSpeed;

    //===========================================Unity============================================
    public override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadComponent(ref this.rb, transform, "LoadRb()");
    }

    protected virtual void Update()
    {
        this.Moving();
    }



    //============================================================================================
    //===========================================Method===========================================
    //============================================================================================

    //============================================Move============================================
    protected virtual void Moving()
    {
        Util.Instance.MovingWithAcceleration(this.rb, InputManager.Instance.MoveDir, this.moveSpeed, this.speedUpTime, this.slowDownTime);
    }
}
