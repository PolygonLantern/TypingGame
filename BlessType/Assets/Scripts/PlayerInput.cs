using UnityEngine;

/// <summary>
/// Class that listens for input from the player
/// </summary>
public class PlayerInput : MonoBehaviour
{ 
    // Reference to the WordManager
    WordManager _wordManager;

    private void Start()
    {
        _wordManager = GetComponent<WordManager>();
    }

    void Update()
    {
        // Loop through each input character and try to type it
        foreach (char key in Input.inputString)
        {
            _wordManager.TypeLetter(key);
        }
    }
}
