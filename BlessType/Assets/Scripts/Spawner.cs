using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;
using Random = UnityEngine.Random;

public class Spawner : MonoBehaviour
{
    public GameObject wordPrefab;

    public DisplayWord SpawnWord(int id)
    {
        GameObject word = Instantiate(wordPrefab);
        word.transform.position = new Vector3(Random.Range(0, 20), 0, Random.Range(0, 20));
        word.GetComponent<WordMat>().wordId = id;
        return word.GetComponent<DisplayWord>();
    }
    
    
}
