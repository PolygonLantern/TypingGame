using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public TMP_Text scoreText;
    public TMP_Text mistakeText;


    private void Update()
    {
        UpdateUI();
    }

    void UpdateUI()
    {
        scoreText.text = $"Score: " + GameManager.Score;
        mistakeText.text = $"Mistakes: " + GameManager.Mistakes;
    }
}
