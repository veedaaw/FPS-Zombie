using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Singleton<T> : MonoBehaviour where T : Singleton<T>
{
    private static T instance;

    public static T Instance { get => instance; set => instance = value; }

    protected virtual void Awake()
    {
        if (instance != null)
        {
            Debug.LogError("[Singleton] trying to create a new instance of singleton class");
        }
        else
        {
            Instance = (T)this;
        }
    }

    protected virtual void OnDestroy()
    {
        if (instance == this)
        {
            instance = null;
        }

    }

    public static bool IsInitialized
    {
        get { return instance != null; }
    }

}
