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
    /// <summary>
    /// Public fields for the text that appears on the screen
    /// </summary>
    public TMP_Text scoreText;
    public TMP_Text mistakeText;

    public GameObject gameOverPanel;
    public TMP_Text gameOverScore;
    public Button restart;
    public Button toMainMenu;

    private SingletonManager _singletonManager;
    private void Awake()
    {
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

    void Restart()
    {
        Scene currentScene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(currentScene.name);
        _singletonManager.GameManager.gameState = GameState.WaveStarted;

    }

    void ToMainMenu()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
        _singletonManager.GameManager.gameState = GameState.Menu;
    }
}
