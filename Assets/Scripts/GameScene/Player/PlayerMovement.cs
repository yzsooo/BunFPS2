using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private CharacterController controller;

    // movement vars
    private Vector3 _velocity;
    private Vector3 _groundVelocity;
    private Vector2 _inputVector;
    private float _actualSpeed;

    [Header("Movement")]
    public float speed = 3.0f;
    public float acceleration = 1.0f;
    public float decelerationMultiplier = 2.0f;
    public float jumpHeight = 3.0f;
    public float fallSpeed = 10.0f;
    
    float _gravity = -9.8f;
    [SerializeField]
    bool _bGrounded;

    private void Awake()
    {
        InitializePlayerMovement();
    }

    void InitializePlayerMovement()
    {
        controller = GetComponent<CharacterController>();
    }

    // get movement vector input
    public void InputMovementVector(Vector2 input)
    {
        _inputVector = input;
    }

    public void InputJump()
    {
        Jump();
    }

    // add vertical velocity if the player is grounded
    void Jump()
    {
        if (!_bGrounded) { return; }
        _velocity.y = Mathf.Sqrt(-jumpHeight * _gravity);
    }

    private void FixedUpdate()
    {
        _bGrounded = controller.isGrounded;
        CalculateActualSpeed();
        ProcessMovement();
        ProcessGravity();
    }

    // calculate the actual speed of the player accounting for acceleration and deceleration
    void CalculateActualSpeed()
    {
        // accelerate if movement key is held
        if (_inputVector.magnitude > 0)
        {
            _actualSpeed += acceleration * Time.deltaTime;
        }
        // decelerate if movement key is let go
        if (_inputVector.magnitude == 0)
        {
            _actualSpeed -= acceleration * decelerationMultiplier * Time.deltaTime;
        }
        _actualSpeed = Mathf.Clamp(_actualSpeed, 0, speed);
    }

    // use the character controller to move the player 
    void ProcessMovement()
    {
        Vector3 moveDircetion = GetGroundVelocity();
        controller.Move(transform.TransformDirection(moveDircetion) * _actualSpeed * Time.deltaTime);
    }
    // return the velocity of the player on ground
    Vector3 GetGroundVelocity()
    {
        // update ground velocity if movement key is held
        if (_inputVector.magnitude > 0)
        {
            _groundVelocity.x = _inputVector.x;
            _groundVelocity.z = _inputVector.y;
        }
        // set ground velocity to 0 when the player is completely stopped
        if (_actualSpeed <= 0)
        {
            _groundVelocity *= 0;
        }
        return _groundVelocity;
    }


    // set the vertical velocity of the player by gravity
    void ProcessGravity()
    {
        // set downwards velocity so that the player is grounded
        if (_bGrounded && _velocity.y < 0)
        {
            _velocity.y = -2.0f;
        }
        // increase fall speed when the player is past the peak of their jump
        if (!_bGrounded && _velocity.y <= jumpHeight)
        {
            _velocity.y -= fallSpeed * Time.deltaTime;
        }
        _velocity.y += _gravity * Time.deltaTime;
        controller.Move(_velocity * Time.deltaTime);
    }
}
