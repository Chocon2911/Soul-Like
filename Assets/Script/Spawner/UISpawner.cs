using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UISpawner : Spawner
{
    private static UISpawner instance;
    public static UISpawner Instance => instance;

    protected override void Awake()
    {
        if (instance != null)
        {
            Debug.LogError("One UISpawner Only (Instance)", instance.gameObject);
            Debug.LogError("One UISpawner Only (currObj)", gameObject);
            return;
        }

        instance = this;
        base.Awake();
    }
}
