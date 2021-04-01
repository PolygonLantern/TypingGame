using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SingletonManager : MonoBehaviour
{
    public static SingletonManager i;

    public Material[] materials;

    private void Awake()
    {
        if (i != null)
        {
            Destroy(gameObject);    
        }
        else
        {
            i = this;
            DontDestroyOnLoad(gameObject);
        }
    }
    
}
