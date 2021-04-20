using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Single class that is singleton and holds information about all other classes that need accessing without having to reference them in the inspector
/// </summary>
/// DefaultExecutionOrder is set to such a low value to assure that this class will run before any other class that might need information from the singleton manager
[DefaultExecutionOrder(-5)]
public class SingletonManager : MonoBehaviour
{
    // Singleton instance
    public static SingletonManager Instance;
    
    // Reference to the materials for the "word"'s colours
    public Material[] materials;
    
    // References to all the classes
    public EventSystem EventSystem;
    public GameManager GameManager;
    public WordManager WordManager;
    public WordGenerator WordGenerator;
    public UIManager UIManager;

    //Check to make sure there is no other singleton manager running
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
    
    /// <summary>
    /// OnEnable runs right after this gameObject is activated. Then it subscribes to the event in the SceneManager to see if there is a loaded scene
    /// </summary>
    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnFinishedLoading;
    }
    
    /// <summary>
    /// The rule of thumb when dealing with events is as follows - Subscribe to an event, you must unsubscribe as well to the same event to prevent memory leakage
    /// </summary>
    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnFinishedLoading;
    }
    
    /// <summary>
    /// This method is what is called when the event is fired. The parameters given are parameters that follow the delegate of the sceneLoaded event.
    /// Once a new scene is loaded the SingletonManager will find all other managers, since they can not be referenced in the inspector from the main menu scene, because they do not exist
    /// </summary>
    /// <param name="scene"></param>
    /// <param name="mode"></param>
    void OnFinishedLoading(Scene scene, LoadSceneMode mode)
    {
        EventSystem = FindObjectOfType<EventSystem>();
        GameManager = FindObjectOfType<GameManager>();
        WordManager = FindObjectOfType<WordManager>();
        UIManager = FindObjectOfType<UIManager>();
    }
}
