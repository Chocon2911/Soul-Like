using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HaThu : HuyMonoBehaviour
{
    //==========================================Variable==========================================
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private SpriteRenderer model;
    [SerializeField] private float moveSpeed;
    [SerializeField] private Vector2 moveDir;

    //===========================================Unity============================================
    void Start()
    {
        // Run when game start
    }

    void Update()
    {
        this.Moving();
    }

    private void FixedUpdate()
    {
        // run every 0.02 seconds
    }

    protected override void Awake()
    {
        // Run when game start
    }

    protected override void OnEnable()
    {
        // Run when game object is turn on
    }

    protected override void OnDisable()
    {
        base.OnDisable();
        // Run when game object is turn off
    }

    public override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadComponent(ref this.rb, transform, "LoadRb()");
        this.LoadComponent(ref this.model, transform.Find("Model"), "LoadModel()"); 
    }

    //===========================================Method===========================================
    private void Moving()
    {
        int yDir;
        int xDir;
        
        
        if (Input.GetKey(KeyCode.W)) yDir = 1;
        else if (Input.GetKey(KeyCode.S)) yDir = -1;
        else yDir = 0;

        
        if (Input.GetKey(KeyCode.D)) xDir = 1;
        else if (Input.GetKey(KeyCode.A)) xDir = -1;
        else xDir = 0;

        this.moveDir = new Vector2(xDir, yDir);
        this.rb.velocity = this.moveDir * this.moveSpeed;
    }
}
