using UnityEngine;
using Random = UnityEngine.Random;

/// <summary>
/// Class that spawns the pokemon model with the text over the head
/// </summary>
public class Spawner : MonoBehaviour
{
    // The pokemon's prefab utilizing an array to let us chose how many models we want to use
    public GameObject[] wordPrefab;

    // Maximum range that the objects can be spawned
    public int maxRange = 30;
    
    /// <summary>
    /// Method that will set the position of the object and will give it an id, as well as speed
    /// </summary>
    /// <param name="id"></param>
    /// <param name="word"></param>
    /// <returns></returns>
    public DisplayWord GetDisplayWord(int id, GameObject word)
    {
        word.transform.position = new Vector3(Random.Range(-20, 20), .5f, Random.Range(-5, maxRange));
        word.GetComponent<WordMat>().wordId = id;
        word.GetComponent<EnemyController>().speed = Random.Range(1,2);
        return word.GetComponent<DisplayWord>();
    }
    
    /// <summary>
    /// Method that instantiates the pokemon's object
    /// </summary>
    /// <returns></returns>
    public GameObject SpawnWord()
    {
        return Instantiate(wordPrefab[Random.Range(0, wordPrefab.Length)], transform);
    }
    
}
