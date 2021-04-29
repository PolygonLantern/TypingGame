using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Enum to make keeping track of the game states easier
/// </summary>
public enum GameState
{
   WaveStarted,
   WaveCompleted,
   Menu,
   GameOver
}

/// <summary>
/// Probably one of the most complicated classes so far in this project.
/// This class handles the spawning of the objects with words and modifying the height of the words on the objects, the game state and the spawning of the waves
/// </summary>
public class GameManager : MonoBehaviour
{
    public AudioClip waveStartClip;
    public AudioClip gameOverClip;
    public AudioSource theme;
    private AudioSource source;

   // The score variable that is modified upon completion of a word. This is also used in the UIManager so it can be displayed on the UI 
   public static int Score;
   
   // Mistakes variable that is modified upon 3 mistakes on a single word or when a word reaches the bar. This is also used in the UIManager so it can be displayed on the UI 
   public static int Mistakes;
   
   // Reference to the GameState enum
   public GameState gameState;

   // List that stores the spawned objects' positions
   private List<Vector2> _enemyPosition = new List<Vector2>();
   
   // Dictionary that stores the word height position
   private Dictionary<int, float> _currentPositions = new Dictionary<int, float>();
   
   // Reference to the singleton manager
   private SingletonManager _singletonManager;
   
   // Variable that is responsible for the amount of words showed on the screen. Modified upon reaching certain score
   private int _spawnedWordsPerTurn;

    private bool checkFinish;
   
   private void Start()
   {
        ResumeGame();
        checkFinish = true;
      // Getting the AudioSource for this gameObject
      source = GetComponent<AudioSource>();

      // Setting the variables 
      Score = 0;
      Mistakes = 0;
      _singletonManager = SingletonManager.Instance;
      
      // Setting the game state to be to Wave Started
      gameState = GameState.WaveStarted;
  
      // Starts the coroutine that handles spawning
      StartCoroutine(nameof(StartWave));
      
      // The following three lines are to update the height of the first wave. This is simultaneously executed while the coroutine is running
      WordManager.FirstRun = true;
      _singletonManager.EventSystem.UpdateTextHeight();
      WordManager.FirstRun = false;
   }

   private void Update()
   {
      // Check every frame if the words are all typed out, if they are, change the state to WaveCompleted
      if (transform.childCount <= 0)
      {
         gameState = GameState.WaveCompleted;
      }

      // Check every frame if the mistakes that the player has made are not greater or equal to 5, if they are then set the game state to GameOver
      if (Mistakes >= 5 && checkFinish)
      {
         gameState = GameState.GameOver;
            source.clip = gameOverClip;
            source.Play();
            theme.Stop();
            checkFinish = false;
            PauseGame();

        }

            // If the game state is GameOver, the Game Over UI panel will appear on the screen
            _singletonManager.UIManager.gameOverPanel.SetActive(gameState == GameState.GameOver);

   }

   /// <summary>
   /// Method to sort the height of the text that appears over the objects.
   /// This method gets the list _enemyPosition and sorts. Looks like that because the Sort method requires custom Comparer when
   /// sorting another data structure. Vector 2 was not supported so I created this comparer.
   /// </summary>
   /// <param name="enemy">The object that the text will be adjusted of</param>
   public void OrderTextHeight(GameObject enemy)
   {
      // Set the initial position of the words
      float yPosition = 3f;
      
      _enemyPosition.Sort(delegate(Vector2 vectorA, Vector2 vectorB)
      {
         if (vectorA.x == null && vectorA.y == null && vectorB.x == null && vectorB.y == null) return 0;
         else if (vectorA.x == null || vectorA.y == null) return -1;
         else if (vectorB.x == null || vectorB.y == null) return 1;
         else return vectorA.y.CompareTo(vectorB.y);
      });
      
      // Reference to the Display Word that is used to display the word, to short the typing and also not call enemy.GetComponent every time
      DisplayWord enemyDw = enemy.GetComponent<DisplayWord>();
      
      // Variable to store the amount of entries in the list _enemyPosition
      int count = _enemyPosition.Count;

      // Loops for each entry in the _enemyPosition
      for (int i = 0; i <= count - 1; i++)
      {
         // Grabs the current position and compares it to the last position in the list and also checks if its the first
         // time the loop runs
         if (_enemyPosition[i].y <= _enemyPosition[count - 1].y && WordManager.FirstRun)
         {
            // Increase the initial position with 2
            yPosition += 2f;
            
            // Check if the dictionary contains the display word of the current enemy. If it does not it adds it to the
            // Dictionary with key being the id of the Display Word, and the value the yPosition value
            if (!_currentPositions.ContainsKey(enemyDw.id))
            {
               _currentPositions.Add(enemyDw.id, yPosition);
            }
            // But if it exist, it sets the last position to the current enemy
            else
            {
               _currentPositions[enemyDw.id] = yPosition;
            }
         }
         // If its not the first run of the loop, this means that the words has been ordered, so now whenever the player
         // types a word correctly the rest of the words will move slightly down to prevent from words disappearing due to
         // being too close to the camera
         else if (!WordManager.FirstRun)
         {
            // Check if the position of the word is not less than 2.5 this means that the word is far from clipping with the ground
            if (yPosition <= 2.5f)
            {
               _currentPositions[enemyDw.id] = yPosition;
            }
            else
            {
               // Lower the position of the text on the y axis
               yPosition = _currentPositions[enemyDw.id] - .15f;
               _currentPositions[enemyDw.id] = yPosition;
            }
            
         }
         
         // Vector that will store the current rectTransform position of the word
         Vector3 wordPosition = enemyDw.text.rectTransform.position;
         
         // Since its impossible to modify a vector, its required a new vector 3 to be created with the modified position
         Vector3 newWordPosition = new Vector3(wordPosition.x, yPosition , wordPosition.z);
         
         // After the check assign the value to the position of the text
         enemyDw.text.rectTransform.position = newWordPosition;
      }
   }
   
   /// <summary>
   /// Method that will add the passed gameObject's position to the _enemyPosition
   /// </summary>
   /// <param name="enemy"></param>
   public void AddEnemyPosition(GameObject enemy)
   {
      _enemyPosition.Add(new Vector2(GetEnemyPosition(enemy).x, GetEnemyPosition(enemy).z));
   }
   
   /// <summary>
   /// Method that clears both the dictionary and the list of positions
   /// </summary>
   void ClearLists()
   {
      _enemyPosition.Clear();
      _currentPositions.Clear();
   }

   /// <summary>
   /// Method that removes an entry from the list by the passed id
   /// </summary>
   /// <param name="id"></param>
   public void RemoveWordFromDictionary(int id)
   {
      _currentPositions.Remove(id);
   }
   
   /// <summary>
   /// Returns the current position of the passed gameObject
   /// </summary>
   /// <param name="enemy"></param>
   /// <returns></returns>
   Vector3 GetEnemyPosition(GameObject enemy)
   {
      return enemy.transform.position;
   }
   
   /// <summary>
   /// IEnumerator that is responsible for the game loop. Runs until the game state is not GameOver, checks the score and based on the score
   /// spawns x amount words.
   /// </summary>
   /// <returns></returns>
   public IEnumerator StartWave()
   {
      
      while (gameState != GameState.GameOver)
      {
         // This might be redundant, but I like to keep it here just in case. This checks if the score and sets the amount of spawnedWordsPerTurn to certain amount
         if (gameState != GameState.GameOver)
         {
            if (Score < 20)
            {
               _spawnedWordsPerTurn = 5;
            }
            else if (Score < 40)
            {
               _spawnedWordsPerTurn = 10;
            }

            // If the game state is WaveStarted, it spawns the words
            if (gameState == GameState.WaveStarted)
            {
               source.clip = waveStartClip;
               source.Play();
                  
               for (int i = 0; i <= _spawnedWordsPerTurn; i++)
               {
                  _singletonManager.EventSystem.SpawnWord(i);
               }
               
               // After the words are spawned, the first run is set to true in order to sort the order of the text for the newly spawned words
               // then it orders them and sets first run again to false, this way the words will be going down.
               WordManager.FirstRun = true;
               _singletonManager.EventSystem.UpdateTextHeight();
               WordManager.FirstRun = false;
            }
            
            // Pausing the Coroutine until any of the conditions are met
            yield return new WaitUntil(()=> gameState == GameState.WaveCompleted || gameState == GameState.GameOver);
            
            // If the wave is completed, clear the list with positions and wait for 5 seconds, then set the state to WaveStarted
            if (gameState == GameState.WaveCompleted)
            {
               ClearLists();
               yield return new WaitForSeconds(4f);
               gameState = GameState.WaveStarted;
            }
         }

      }
   }

    public void ResumeGame()
    {
        Time.timeScale = 1;
    }

    public void PauseGame()
    {
        Time.timeScale = 0;
    }
}
