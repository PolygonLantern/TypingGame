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
    
    // Check for the collision. The range is the distance between the groundCheck gameObject and the groundLayered object 
    private float _range;
    
    // The speed of the character
    public float speed;
    
    // The game object that is placed at the bottom of the character to check if the character is grounded
    public Transform groundCheck;

    // The layer that the bool isGrounded need to check against to see if the character is on the ground
    public LayerMask ground;
    
    private void Start()
    {
        // Getting a reference to the controller 
        _controller = GetComponent<CharacterController>();
        _range = .4f;
    }

    private void Update()
    {   
        // The character will be grounded if the sphere that is projected from the GroundCheck gameObject collides with an object that is Layered as a Ground
        _isGrounded = Physics.CheckSphere(groundCheck.position, _range, ground, QueryTriggerInteraction.Ignore);
        
        // If the player is on the ground and the velocity is less than 0 it sets the velocity to 0
        if (_isGrounded && _velocity.y < 0)
        {
            _velocity.y = 0;
        }
        
        // Movement vector
        Vector3 movement = -Vector3.forward;
        
        // Making the object move forward, modified by the speed and multiplied by Time.deltaTime to make it framerate independant
        _controller.Move(movement * speed * Time.deltaTime);
        
        // Every frame recalculate the velocity
        _velocity.y += _gravity * Time.deltaTime;
        
        // Every frame move the character towards the velocity
        _controller.Move(_velocity * Time.deltaTime);
    }
}
