using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
   public static int Score;
   public static int Mistakes;

   private List<Vector2> _enemyPosition = new List<Vector2>();
   
   private void Start()
   {
      Score = 0;
      Mistakes = 0;
   }

   public void OrderTextHeight(GameObject enemy)
   {
      float yPosition = 2.5f;
      _enemyPosition.Sort(delegate(Vector2 vectorA, Vector2 vectorB)
      {
         if (vectorA.x == null && vectorA.y == null && vectorB.x == null && vectorB.y == null) return 0;
         else if (vectorA.x == null || vectorA.y == null) return -1;
         else if (vectorB.x == null || vectorB.y == null) return 1;
         else return vectorA.y.CompareTo(vectorB.y);
      });

      int count = _enemyPosition.Count;

      for (int i = 0; i <= count - 1; i++)
      {
         if (_enemyPosition[i].y < _enemyPosition[count - 1].y)
         {
            yPosition += 2.5f;
         }
         else
         {
            
         }
         
         Vector3 wordPosition = enemy.GetComponent<DisplayWord>().text.rectTransform.position;
         Vector3 newWordPosition = new Vector3(wordPosition.x, yPosition , wordPosition.z);
         enemy.GetComponent<DisplayWord>().text.rectTransform.position = newWordPosition;
      }
      
   }
   public void AddTextHeight(GameObject enemy)
   {
      _enemyPosition.Add(new Vector2(GetEnemyPosition(enemy).x, GetEnemyPosition(enemy).z));
   }

   Vector3 GetEnemyPosition(GameObject enemy)
   {
      return enemy.transform.position;
   }
   
}
