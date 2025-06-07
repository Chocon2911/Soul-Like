using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Singleton<T> : HuyMonoBehaviour where T : Singleton<T>
{
    private static T instance;
    public static T Instance => instance;

    protected override void Awake()
    {
        base.Awake();
        if (instance != null)
        {
            Debug.LogError($"One [Spawner<T{typeof(T).Name}>] only (currObj)", gameObject);
            Debug.LogError($"One [Spawner<T{typeof(T).Name}>] only (instance)", gameObject);
            return;
        }

        instance = this as T;
    }
}
