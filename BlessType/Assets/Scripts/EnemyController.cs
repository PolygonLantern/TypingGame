using UnityEngine;
/// <summary>
/// Script that makes the word holders move forward towards the player
/// </summary>
public class EnemyController : MonoBehaviour
{
    /// <summary>
    /// Reference to the character controller that the word holder has, and speed variable to make it feel a bit more "realistic"
    /// </summary>
    private CharacterController _controller;
    private float _gravity = -9.81f;
    private bool _isGrounded;
    private Vector3 _velocity;
    private float _range;
    
    public float speed;
    public Transform groundCheck;
    public LayerMask ground;
    
    private void Start()
    {
        // Getting a reference to the controller 
        _controller = GetComponent<CharacterController>();
        _range = .4f;
    }

    private void Update()
    {
        _isGrounded = Physics.CheckSphere(groundCheck.position, _range, ground, QueryTriggerInteraction.Ignore);

        if (_isGrounded && _velocity.y < 0)
        {
            _velocity.y = 0;
        }
        
        Vector3 movement = -Vector3.forward;
        // Making the object move forward, modified by the speed and multiplied by Time.deltaTime to make it framerate independant
        _controller.Move(movement * speed * Time.deltaTime);

        _velocity.y += _gravity * Time.deltaTime;
        _controller.Move(_velocity * Time.deltaTime);
    }
}
