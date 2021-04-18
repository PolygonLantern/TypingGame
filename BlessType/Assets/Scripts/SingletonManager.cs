using UnityEngine;
[DefaultExecutionOrder(-5)]
public class SingletonManager : MonoBehaviour
{
    public static SingletonManager Instance;

    public Material[] materials;

    public EventSystem EventSystem;
    public GameManager GameManager;
    public WordManager WordManager;
    public WordGenerator WordGenerator;
    public UIManager UIManager;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);    
        }
        else
        {
            Instance = this;
        }
    }
}
