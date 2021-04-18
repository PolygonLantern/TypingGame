using System;
using System.Collections.Generic;
using UnityEngine;

public class EventSystem : MonoBehaviour
{
    public Action<int> SpawnWords;
    public Action UpdateTextHeights;
    public Action<int> DeleteTextObjects;
    public Action<string> GetPokemonName;
    public void SpawnWord(int amount)
    {
        SpawnWords?.Invoke(amount);
    }

    public void UpdateTextHeight()
    {
        UpdateTextHeights?.Invoke();
    }

    public void DeleteTextObject(int id)
    {
        DeleteTextObjects?.Invoke(id);
    }

    public void GetName(string str)
    {
        GetPokemonName?.Invoke(str);
    }
}
