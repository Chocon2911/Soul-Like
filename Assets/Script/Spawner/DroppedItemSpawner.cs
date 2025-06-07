using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DroppedItemSpawner : Spawner
{
    //==========================================Variable==========================================
    [Header("===Dropped Item===")]
    private static DroppedItemSpawner instance;
    public static DroppedItemSpawner Instance => instance;

    //===========================================Unity============================================
    protected override void Awake()
    {
        if (instance != null)
        {
            Debug.LogError("One DroppedItemSpawner Only (Instance)", instance.gameObject);
            Debug.LogError("One DroppedItemSpawner Only (currObj)", gameObject);
            return;
        }

        instance = this;
        base.Awake();
    }
}
