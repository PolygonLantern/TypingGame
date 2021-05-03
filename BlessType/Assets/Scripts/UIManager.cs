using System;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

/// <summary>
/// Class that takes care of the UI in the game
/// </summary>
public class UIManager : MonoBehaviour
{
    // The AudioSource utilized to play the UI buttons
    public AudioSource UIsource;

    /// <summary>
    /// Public fields for the text that appears on the screen
    /// </summary>
    public TMP_Text scoreText;
    public TMP_Text mistakeText;

    /// <summary>
    /// Fields for the Game Over panel
    /// </summary>
    public GameObject gameOverPanel;
    public TMP_Text gameOverScore;
    public Button restart;
    public Button toMainMenu;

    private SingletonManager _singletonManager;
    private void Awake()
    {
        // Subscribe the two buttons to the corresponding methods
        restart.onClick.AddListener(Restart);
        toMainMenu.onClick.AddListener(ToMainMenu);
    }

    private void Start()
    {
        _singletonManager = SingletonManager.Instance;
    }

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
        gameOverScore.text = $"Your Score is " + GameManager.Score + ". Great Job!";
    }
    
    /// <summary>
    /// Method that is used by the Restart button, gets the current scene and loads it again, also resets the game state to wave started,
    /// so it can spawn the new objects
    /// </summary>
    void Restart()
    {
        UIsource.Play();
        Scene currentScene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(currentScene.name);
        _singletonManager.GameManager.gameState = GameState.WaveStarted;

    }

    /// <summary>
    /// Method that is used by the To Main Menu button, that takes the currently loaded scene's index and loads the scene that is before it. Then changes the game state to menu, which currently
    /// does nothing special
    /// </summary>
    void ToMainMenu()
    {
        UIsource.Play();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
        _singletonManager.GameManager.gameState = GameState.Menu;
    }
}
