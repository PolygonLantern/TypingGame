using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    WordManager _wordManager;

    private void Start()
    {
        _wordManager = GetComponent<WordManager>();
    }

    // Update is called once per frame
    void Update()
    {
        foreach (char key in Input.inputString)
        {
            _wordManager.TypeLetter(key);
        }
    }
}
