using System;
using UnityEngine;

/// <summary>
/// Script that takes care of what happens when the object collide with a certain area that will kill the object
/// </summary>
public class DeathTrigger : MonoBehaviour
{
    private SingletonManager _singletonManager;

    private void Start()
    {
        _singletonManager = SingletonManager.Instance;
    }

    private void OnTriggerEnter(Collider other)
    {
        // If there was a word that the player was typing but couldnt make it, it just switches off from that word allowing
        //it to listen for another word whenever another key is pressed.
        WordManager.HasActiveWord = false;

        // Remove the word from the list to prevent null references from the unfinished word
        _singletonManager.WordManager.words.Remove(_singletonManager.WordManager.activeWord);
        
        // Increase the mistakes counter
        GameManager.Mistakes++;
        
        _singletonManager.GameManager.RemoveWordFromDictionary(other.GetComponent<DisplayWord>().id);
        _singletonManager.WordManager.RemoveWordFromList(other.gameObject);
        
        // Destroys the gameObject in 1 second
        Destroy(other.gameObject, 1f);
    }
}
