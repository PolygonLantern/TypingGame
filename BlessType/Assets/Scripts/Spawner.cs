using UnityEngine;
using Random = UnityEngine.Random;

public class Spawner : MonoBehaviour
{
    public GameObject wordPrefab;
    public int maxRange = 30;
    public DisplayWord GetDisplayWord(int id, GameObject word)
    {
        word.transform.position = new Vector3(Random.Range(-20, 20), .5f, Random.Range(-5, maxRange));
        word.GetComponent<WordMat>().wordId = id;
        word.GetComponent<EnemyController>().speed = Random.Range(1,2);
        return word.GetComponent<DisplayWord>();
    }

    public GameObject SpawnWord()
    {
        return Instantiate(wordPrefab, transform);
    }
    
}
