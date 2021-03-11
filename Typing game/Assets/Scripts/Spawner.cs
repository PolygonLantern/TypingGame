using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject wordPrefab;
    public Transform canvasTransform;

    public DisplayWord SpawnWord()
    {
        GameObject word = Instantiate(wordPrefab, canvasTransform);

        return word.GetComponent<DisplayWord>();
    }
}
