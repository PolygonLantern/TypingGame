using UnityEngine;

public class SingletonManager : MonoBehaviour
{
    public static SingletonManager Instance;

    public Material[] materials;

    public EventSystem EventSystem;
    public GameManager GameManager;
    public WordManager WordManager;
    
    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);    
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }
}
