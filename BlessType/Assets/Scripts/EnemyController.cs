using UnityEngine;

public class EnemyController : MonoBehaviour
{
    private CharacterController _controller;
    public float speed;
    private void Start()
    {
        _controller = GetComponent<CharacterController>();
    }

    private void Update()
    {
        _controller.Move(-Vector3.forward * speed * Time.deltaTime);
    }
}
