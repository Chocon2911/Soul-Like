using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : HuyMonoBehaviour
{
    //==========================================Variable==========================================
    private static GameManager instance;
    public static GameManager Instance => instance;

    [SerializeField] private Player player;
    public Player Player => player;

    //===========================================Unity============================================
    protected override void Awake()
    {
        if (instance != null)
        {
            Debug.LogError("instance not null (transform)", transform.gameObject);
            Debug.LogError("Instance not null (instance)", instance.gameObject);
            return;
        }

        instance = this;
        base.Awake();
    }
}
