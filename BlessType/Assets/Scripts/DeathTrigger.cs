using UnityEngine;

public class DeathTrigger : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        WordManager.HasActiveWord = false;
        GameManager.Mistakes++;
        Destroy(other.gameObject, 1f);
    }
}
