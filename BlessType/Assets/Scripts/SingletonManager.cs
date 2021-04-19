using System;
using UnityEngine;
using UnityEngine.SceneManagement;

[DefaultExecutionOrder(-5)]
public class SingletonManager : MonoBehaviour
{
    public static SingletonManager Instance;

    public Material[] materials;

    public EventSystem EventSystem;
    public GameManager GameManager;
    public WordManager WordManager;
    public WordGenerator WordGenerator;
    public UIManager UIManager;

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

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnFinishedLoading;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnFinishedLoading;
    }

    void OnFinishedLoading(Scene scene, LoadSceneMode mode)
    {
        EventSystem = FindObjectOfType<EventSystem>();
        GameManager = FindObjectOfType<GameManager>();
        WordManager = FindObjectOfType<WordManager>();
        UIManager = FindObjectOfType<UIManager>();
    }
}
