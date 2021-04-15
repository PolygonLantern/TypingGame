using System;
using System.Collections.Generic;
using UnityEngine;

public class EventSystem : MonoBehaviour
{
    public Action SpawnWords;
    public Action UpdateTextHeights;
    public Action DeleteTextObjects;
    public void SpawnWord()
    {
        SpawnWords?.Invoke();
    }

    public void UpdateTextHeight()
    {
        UpdateTextHeights?.Invoke();
    }

    public void DeleteTextObject()
    {
        DeleteTextObjects?.Invoke();
    }
}
