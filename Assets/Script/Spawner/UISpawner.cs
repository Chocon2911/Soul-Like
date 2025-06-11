using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum UISpawnCode
{
    NONE = 0,
    INVENTORY_SLOT = 1,
    INVENTORY_ITEM = 2
}

public static class UISpawnCodeExtension
{
    public static string GetCodeStr(this UISpawnCode code)
    {
        switch (code)
        {
            case UISpawnCode.INVENTORY_SLOT:
                return "InventorySlot";
            case UISpawnCode.INVENTORY_ITEM:
                return "InventoryItem";
            default:
                return "None";
        }
    }
}

public class UISpawner : Spawner
{
    //==========================================Variable==========================================
    private static UISpawner instance;
    public static UISpawner Instance => instance;

    //===========================================Unity============================================
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

    //===========================================Method===========================================
    public Transform SpawnByCode (UISpawnCode code, Vector3 spawnPos, Quaternion spawnRot)
    {
        return this.SpawnByName(code.GetCodeStr(), spawnPos, spawnRot);
    }
}
