using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Singleton<T> : MonoBehaviour where T : Singleton<T>
{
    private static T _instance;
    public static bool initialized;
    public static T Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<T>();
                if (FindObjectsOfType<T>().Length > 1)
                {
                    return _instance;
                }
                if (_instance == null)
                {
                    string name = typeof(T).Name;
                    GameObject obj = GameObject.Find(name);
                    if (obj == null)
                    {
                        obj = new GameObject(name);
                    }
                    _instance = obj.AddComponent<T>();
                    DontDestroyOnLoad(obj);
                }
            }
            initialized = true;
            return _instance;
        }
    }
    protected virtual void Awake()
    {
        if (initialized)
        {
            Destroy(gameObject);
        }
    }

    protected virtual void OnDestroy()
    {
        _instance = null;
        initialized = false;
    }
}
