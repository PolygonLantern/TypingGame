using System;
using UnityEngine;

public class EventSystem : MonoBehaviour
{
    public Action SpawnWords;

    public void SpawnWord()
    {
        SpawnWords?.Invoke();
    }
    
}
