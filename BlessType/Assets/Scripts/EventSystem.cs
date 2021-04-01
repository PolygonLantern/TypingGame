using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventSystem : MonoBehaviour
{
    public static EventSystem Instance;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }
    

    
    public Action<int> IsWordTyped;

    public void WordTyped(int id)
    {
        IsWordTyped?.Invoke(id);
    }

    public Action<int> IsWordMistaken;
    public void WordMistaken(int id)
    {
        IsWordMistaken?.Invoke(id);
    }

    public Action<int> IsWordBeingTyped;
    public void WordIsBeingTyped(int id)
    {
        IsWordBeingTyped?.Invoke(id);
    }

    public Action SpawnWords;

    public void SpawnWord()
    {
        SpawnWords?.Invoke();
    }
    
}
