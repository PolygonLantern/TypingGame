using System.Collections.Generic;
using System.Linq;
using UnityEngine;

/// <summary>
/// Class that manages the word that are displayed on the screen
/// </summary>
[DefaultExecutionOrder(0)]
public class WordManager : MonoBehaviour
{
    // The Audio Clip utilized to play the UI buttons when the player inputs the wrong letter with an active word
    public AudioClip UIwrongSound;

    // Variable that keeps track of how many typos are being made per word
    public static int TimesFailedAWord;
    
    // List that contains the words that will appear on the screen
    public List<Word> words;
    
    // Variable that keeps track if there is an active word
    public static bool HasActiveWord;
    
    // The current active word
    public Word activeWord;
    
    // Variable for the text height sorting
    public static bool FirstRun;
    
    // Reference to the spawner 
    private Spawner _spawner;
    
    // Reference to the singleton manager
    private SingletonManager _singletonManager;
    
    // List of the objects that hold the words above them
    private List<GameObject> _wordsGameObjects = new List<GameObject>();
    
    
    private void Start()
    {
        _singletonManager= SingletonManager.Instance;
        
        // Subscribing to the events for spawning words, updating the height of the text, and deleting the objects that hold the text
        _singletonManager.EventSystem.SpawnWords += SpawnWords;
        _singletonManager.EventSystem.UpdateTextHeights += UpdateText;
        _singletonManager.EventSystem.DeleteTextObjects += DeleteTextObjects;

        // Set the typo counter to 0
        TimesFailedAWord = 0;
        
        // Get the spawner
        _spawner = GetComponent<Spawner>();
    }

    /// <summary>
    /// Whenever this object is destroyed or disabled it unsubscribes to the events below
    /// </summary>
    private void OnDisable()
    {
        _singletonManager.EventSystem.SpawnWords -= SpawnWords;
        _singletonManager.EventSystem.UpdateTextHeights -= UpdateText;
        _singletonManager.EventSystem.DeleteTextObjects -= DeleteTextObjects;
    }

    /// <summary>
    /// Method that instantiates the word in the scene, calls the word constructor and passes it the word, the corresponding display word and its id.
    /// Then adds the word and the object that holds the word to the their lists respectively 
    /// </summary>
    /// <param name="currentID"></param>
    void AddWord(int currentID)
    {
        // Instantiate the object in the scene
        GameObject wordObject = _spawner.SpawnWord();
        
        // Set the word
        Word word = new Word(_singletonManager.WordGenerator.GetRandomName(), _spawner.GetDisplayWord(currentID, wordObject), currentID);
        
        // Set the display word of the current word to the id that has been passed as parameter
        wordObject.GetComponent<DisplayWord>().id = currentID;
        
        // Add the word to the words list
        words.Add(word);
        
        // Add the object to the objects list
        _wordsGameObjects.Add(wordObject);
    }
    
    /// <summary>
    /// Method that checks the character that the player has input and sees if there is a matching word starting with this
    /// character if there is no active word at the moment of input. If there is an active word already, the method calls
    /// GetNextChar to proceed with the word
    /// </summary>
    /// <param name="key"></param>
    public void TypeLetter(char key)
    {
        // Check if there is an active word
        if (HasActiveWord)
        {
            // Check if the input key is matching the next character, if it is, input it
            if (activeWord.GetNextChar() == key)
            {
                activeWord.InputChar();
            }
            // if not, increase the typo counter
            else
            {
                AudioSource source = GetComponent<AudioSource>();
                source.clip = UIwrongSound;
                source.Play();
                ++TimesFailedAWord;
            }
        }
        // if there is no active word
        else
        {   
            // Loop through all the words that are spawned 
            foreach (Word word in words)
            {
                // The first one that matches the input key gets marked as active word
                if (word.GetNextChar() == key)
                {
                    activeWord = word;
                    HasActiveWord = true;
                    word.InputChar();
                    break;
                }
            }
        }
        
        // Check if there is an active word and the word has been typed correctly
        if (HasActiveWord && activeWord.WordTyped())
        {   
            // Reset the active word
            HasActiveWord = false;
            
            // Remove the word from the list
            words.Remove(activeWord);
            
            // Call the events that will delete the word object and will update the height of the rest of the words' text
            _singletonManager.EventSystem.DeleteTextObject(activeWord.id);
            _singletonManager.EventSystem.UpdateTextHeight();
        }
    }
    
    /// <summary>
    /// Method that will be called upon triggering the event SpawnWords
    /// </summary>
    /// <param name="amount"></param>
    void SpawnWords(int amount)
    {
       AddWord(amount);
    }

    /// <summary>
    /// Method that will be called whenever the UpdateTextHeight event is triggered
    /// </summary>
    void UpdateText()
    {
        UpdateHeight(_wordsGameObjects);
    }
    
    /// <summary>
    /// Method that for each word in the given list will add the position of the object and will set the height of the text
    /// </summary>
    /// <param name="testWords"></param>
    void UpdateHeight(List<GameObject> testWords)
    {
        foreach (var word in testWords)
        {
            _singletonManager.GameManager.AddEnemyPosition(word);
            _singletonManager.GameManager.OrderTextHeight(word);
        }
    }
    
    /// <summary>
    /// Method that will be called upon triggering the DeleteTextObject event
    /// </summary>
    /// <param name="id"></param>
    private void DeleteTextObjects(int id)
    {
        foreach (var word in _wordsGameObjects.ToList())
        {
            if (word.GetComponent<DisplayWord>().id == id)
            {
                _wordsGameObjects.Remove(word);
            }
        }
    }
    
    /// <summary>
    /// Method that will remove the passed word from the list of word objects
    /// </summary>
    /// <param name="word"></param>
    public void RemoveWordFromList(GameObject word)
    {
        _wordsGameObjects.Remove(word);
    }
}
