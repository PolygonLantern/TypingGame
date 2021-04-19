using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GameState
{
   WaveStarted,
   WaveCompleted,
   Menu,
   GameOver
}

public class GameManager : MonoBehaviour
{
   public static int Score;
   public static int Mistakes;
   float _waitUntilNextWave = 5f;
   public GameState gameState;

   private List<Vector2> _enemyPosition = new List<Vector2>();
   private Dictionary<int, float> _currentPositions = new Dictionary<int, float>();
   private SingletonManager _singletonManager;
   private int _spawnedWordsPerTurn;

   private bool _firstUpdate;
   private void Start()
   {
      Score = 0;
      Mistakes = 0;
      _singletonManager = SingletonManager.Instance;
      gameState = GameState.WaveStarted;
  
      StartCoroutine(nameof(StartWave));
      WordManager.test = true;
      _singletonManager.EventSystem.UpdateTextHeight();
      WordManager.test = false;
   }

   private void Update()
   {
      if (transform.childCount <= 0)
      {
         gameState = GameState.WaveCompleted;
      }

      if (Mistakes >= 5)
      {
         gameState = GameState.GameOver;
      }

      _singletonManager.UIManager.gameOverPanel.SetActive(gameState == GameState.GameOver);
   }

   public void OrderTextHeight(GameObject enemy)
   {
      float yPosition = 3f;
      _enemyPosition.Sort(delegate(Vector2 vectorA, Vector2 vectorB)
      {
         if (vectorA.x == null && vectorA.y == null && vectorB.x == null && vectorB.y == null) return 0;
         else if (vectorA.x == null || vectorA.y == null) return -1;
         else if (vectorB.x == null || vectorB.y == null) return 1;
         else return vectorA.y.CompareTo(vectorB.y);
      });
      
      DisplayWord enemyDw = enemy.GetComponent<DisplayWord>();
      
      int count = _enemyPosition.Count;

      for (int i = 0; i <= count - 1; i++)
      {
         if (_enemyPosition[i].y <= _enemyPosition[count - 1].y && WordManager.test)
         {
            yPosition += 2f;

            if (!_currentPositions.ContainsKey(enemyDw.id))
            {
               _currentPositions.Add(enemyDw.id, yPosition);
            }
            else
            {
               _currentPositions[enemyDw.id] = yPosition;
            }
            
         }
         else if (!WordManager.test)
         {
            if (yPosition <= 2.5f)
            {
               _currentPositions[enemyDw.id] = yPosition;
            }
            else
            {
               yPosition = _currentPositions[enemyDw.id] - .15f;
               _currentPositions[enemyDw.id] = yPosition;
            }
            
         }
         
         Vector3 wordPosition = enemyDw.text.rectTransform.position;
         Vector3 newWordPosition = new Vector3(wordPosition.x, yPosition , wordPosition.z);
         enemyDw.text.rectTransform.position = newWordPosition;
      }

   }
   public void AddEnemyPosition(GameObject enemy)
   {
      _enemyPosition.Add(new Vector2(GetEnemyPosition(enemy).x, GetEnemyPosition(enemy).z));
   }

   void ClearLists()
   {
      _enemyPosition.Clear();
      _currentPositions.Clear();
   }

   public void RemoveWordFromDictionary(int id)
   {
      _currentPositions.Remove(id);
   }
   Vector3 GetEnemyPosition(GameObject enemy)
   {
      return enemy.transform.position;
   }

   public IEnumerator StartWave()
   {
      
      while (gameState != GameState.GameOver)
      {
         
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

            if (gameState == GameState.WaveStarted)
            {
               for (int i = 0; i <= _spawnedWordsPerTurn; i++)
               {
                  _singletonManager.EventSystem.SpawnWord(i);
               }
               
               WordManager.test = true;
               _singletonManager.EventSystem.UpdateTextHeight();
               WordManager.test = false;
            }
         
            yield return new WaitUntil(()=> gameState == GameState.WaveCompleted || gameState == GameState.GameOver);
            
            
            if (gameState == GameState.WaveCompleted)
            {
               ClearLists();
               yield return new WaitForSeconds(_waitUntilNextWave);
               gameState = GameState.WaveStarted;
            }
         }

      }
   }
}
