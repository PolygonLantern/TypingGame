using UnityEngine;
using Random = UnityEngine.Random;

public class Spawner : MonoBehaviour
{
    public GameObject wordPrefab;

    public DisplayWord GetDisplayWord(int id, GameObject word)
    {
        word.transform.position = new Vector3(Random.Range(0, 20), .5f, Random.Range(0, 20));
        word.GetComponent<WordMat>().wordId = id;
        word.GetComponent<EnemyController>().speed = Random.Range(2, 6);
        return word.GetComponent<DisplayWord>();
    }

    public GameObject SpawnWord()
    {
        return Instantiate(wordPrefab, transform);
    }
    
}
