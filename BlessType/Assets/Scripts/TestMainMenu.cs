using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
/// <summary>
/// Test main menu class, that is used in the Main Menu Scene. This class is just placeholder however can be used to build upon for future UI
/// </summary>
public class TestMainMenu : MonoBehaviour
{
    // The AudioSource utilized to play the UI buttons
    private AudioSource source;

   // Reference to the button on the screen in the first scene
   public Button playButton;
   
   // Bool that listens to the IsReady static variable in the WordGenerator
   private bool _readyToLoadLevel;
   
   // Reference to the singletonManager to get access to the LoadPokemon method
   private SingletonManager _singletonManager;

   private void Start()
   {
        source = GetComponent<AudioSource>();

      _singletonManager = SingletonManager.Instance;
      // Check if the isReady is true, and if its not call the LoadPokemon method to start doing requests to the pokemon API
      if (!_readyToLoadLevel)
      {
         _singletonManager.WordGenerator.LoadPokemon();
      }
      // Switch the button off, to prevent the game to load before the pokemon has been initialised
      playButton.interactable = false;
      
      // AddListener technically subscribes to the method that is passed when the button is clicked
      playButton.onClick.AddListener(LoadWords);
   }
   
   /// <summary>
   /// Method that checks if the isReady is true, and if it is, it loads the next level
   /// </summary>
   void LoadWords()
   {
        source.Play();
        if (_readyToLoadLevel)
      {

         SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
      }
   }

   private void Update()
   {  
      // Constantly check for changes to the isReady variable
      _readyToLoadLevel = WordGenerator.IsReady;
      
      // Check if the isReady is true, then set the button on again
      if (_readyToLoadLevel)
      {
         playButton.interactable = true;
      }
   }
}
