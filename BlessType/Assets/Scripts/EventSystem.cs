using System;
using UnityEngine;

/// <summary>
/// Class that stores all the events in the game
/// </summary>
public class EventSystem : MonoBehaviour
{
    // Event that takes int argument used to spawn words
    public Action<int> SpawnWords;
    
    // Event to update the height of the appearing text over the objects
    public Action UpdateTextHeights;
    
    // Event that helps managing the deletion of the objects
    public Action<int> DeleteTextObjects;
    
    /// <summary>
    /// Method to invoke the event. This method is called when the event should be called
    /// </summary>
    /// <param name="amount">Amount of words to spawn</param>
    public void SpawnWord(int amount)
    {
        SpawnWords?.Invoke(amount);
    }

    /// <summary>
    /// Method to call the UpdateTextHeight event. This method is called when the event should be called
    /// </summary>
    public void UpdateTextHeight()
    {
        UpdateTextHeights?.Invoke();
    }
    
    /// <summary>
    /// Method to call the DeleteTextObject event. This method is called when the event should be called
    /// </summary>
    /// <param name="id">The id of the object that needs to be deleted</param>
    public void DeleteTextObject(int id)
    {
        DeleteTextObjects?.Invoke(id);
    }
}
