using TMPro;
using UnityEngine;
/// <summary>
/// Class that takes care of the UI in the game
/// </summary>
public class UIManager : MonoBehaviour
{
    /// <summary>
    /// Public fields for the text that appears on the screen
    /// </summary>
    public TMP_Text scoreText;
    public TMP_Text mistakeText;


    private void Update()
    {
        UpdateUI();
    }

    /// <summary>
    /// Called in update every frame, it updates the text on the screen to the current score and mistakes of the player
    /// </summary>
    void UpdateUI()
    {
        scoreText.text = $"Score: " + GameManager.Score;
        mistakeText.text = $"Mistakes: " + GameManager.Mistakes;
    }
}
