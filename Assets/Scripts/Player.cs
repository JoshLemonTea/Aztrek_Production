using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Player : MonoBehaviour
{
    private CharacterController _characterController;

    [SerializeField] private float _moveSpeed;
    public float MoveSpeed { get => _moveSpeed; set => _moveSpeed = value; }

    public float GravityValue { get; set; }

    [SerializeField] private float _jumpHeight;
    public float JumpHeight { get => _jumpHeight; set => _jumpHeight = value; }

    [SerializeField] private float _addedJumpForce;
    [SerializeField] private float _addedJumpFalloff;
    private float _currentAddedJumpForce;

    private float JumpForce { get { return Mathf.Sqrt(-2f * GravityValue * JumpHeight); } }

    [SerializeField] private float _acceleration;

    [SerializeField] private float _decceleration;

    public float Acceleration { get => _acceleration; set => _acceleration = value; }

 
    public float Deceleration { get => _decceleration; set => _decceleration = value; }


    [SerializeField] private float _airAcceleration = 10;

    [SerializeField] private float _airDeceleration = 10f;

    public float AirDeceleration { get => _airDeceleration; set => _airDeceleration = value; }

    public float AirAcceleration { get => _airAcceleration; set => _airAcceleration = value; }

    //Changed this for the bouncy platform script
    [HideInInspector] public Vector3 _movement;

    public bool IsGrounded { get { return _characterController.isGrounded; } }

    public bool IsGoingUp { get { return _movement.y > 0f; } }

    public bool CanJump { get; set; }

    //Added this for the bouncy platform script
    [HideInInspector] public float lastGroundHeight;

    [SerializeField] private float coyoteTime;
    private float coyoteTimer;

    private void Update()
    {
        if (IsGrounded)
        {
            lastGroundHeight = transform.position.y;
        }

        if (coyoteTimer > 0)
        {
            HandleCoyoteTime();
        }
    }

    public void OnEnter()
    {
        _characterController = GetComponent<CharacterController>();

        //Increased Gravity gives snappier jumps
        GravityValue = Physics.gravity.y * 6;

        transform.position = Vector3.zero;
    }

    public void Move(Vector3 moveInput)
    {
        Vector3 relativeInput = RelativeMovementVector(moveInput);

        if (IsGrounded)
        {
            if(moveInput.magnitude > 0f)
            {
                _movement.x = Mathf.MoveTowards(_movement.x, relativeInput.x * MoveSpeed, Acceleration * Time.deltaTime);
                _movement.z = Mathf.MoveTowards(_movement.z, relativeInput.y * MoveSpeed, Acceleration * Time.deltaTime);
            }
            else
            {
                _movement.x = Mathf.MoveTowards(_movement.x, relativeInput.x * MoveSpeed, Deceleration * Time.deltaTime);
                _movement.z = Mathf.MoveTowards(_movement.z, relativeInput.y * MoveSpeed, Deceleration * Time.deltaTime);
            }
        }
        else
        {
            if(moveInput.magnitude > 0f)
            {


                _movement.x = Mathf.MoveTowards(_movement.x, relativeInput.x * MoveSpeed, AirAcceleration * Time.deltaTime);
                _movement.z = Mathf.MoveTowards(_movement.z, relativeInput.y * MoveSpeed, AirAcceleration * Time.deltaTime);
            }
            else
            {
                _movement.x = Mathf.MoveTowards(_movement.x, relativeInput.x * MoveSpeed, AirDeceleration * Time.deltaTime);
                _movement.z = Mathf.MoveTowards(_movement.z, relativeInput.y * MoveSpeed, AirDeceleration * Time.deltaTime);
            }
        }

        _characterController.Move(_movement * Time.deltaTime);
    }

    public void WallRun(Vector3 wallrunMovement)
    {
        _characterController.Move(wallrunMovement * Time.deltaTime);
    }

    public void ApplyGravity()
    {
        if (!IsGrounded)
        {
            _movement.y += GravityValue * Time.deltaTime;
        }
        else
        {
            float defaultDownwardForce = -1f;
            _movement.y = defaultDownwardForce;
        }
    }

    public void Jump()
    {
        if (coyoteTime == 0)
        {
            CanJump = IsGrounded;
        }

        if (CanJump)
        {
            _movement.y = JumpForce;
            _currentAddedJumpForce = -GravityValue * _addedJumpForce;
        }
        else
        {
            if (_movement.y > 0.2f)
            {
                _currentAddedJumpForce -= Time.deltaTime * _addedJumpFalloff;
                _movement.y += _currentAddedJumpForce * Time.deltaTime;
            }
        }

        //Immediately disable Coyote Time
        coyoteTimer = 0;
    }

    public void FaceForward(Vector2 moveInput)
    {
        if(moveInput != Vector2.zero)
        transform.forward = new Vector3(_movement.x, 0f, _movement.z);
    }
    
    public void AddMovement(Vector3 movement)
    {
        _movement += movement;
    }

    public void AdjustYMovement(float yMovement)
    {
        _movement.y = yMovement;
    }

    //Function that calculates the movement input relative to the active camera
    private Vector3 RelativeMovementVector(Vector3 moveInput)
    {
        float horizontalAxis = moveInput.x;
        float verticalAxis = moveInput.y;

        //camera forward and right vectors:
        var forward = Camera.main.transform.forward;
        var right = Camera.main.transform.right;

        //project forward and right vectors on the horizontal plane (y = 0)
        forward.y = 0f;
        right.y = 0f;
        forward.Normalize();
        right.Normalize();

        //this is the direction in the world space we want to move:
        var desiredMoveDirection = forward * verticalAxis + right * horizontalAxis;
        desiredMoveDirection.y = desiredMoveDirection.z;
        desiredMoveDirection.z = 0;
        return desiredMoveDirection;
    }

    private void HandleCoyoteTime()
    {
        CanJump = false;

        if (IsGrounded)
        {
            coyoteTimer = coyoteTime;
        }
        else
        {
            coyoteTimer -= Time.deltaTime;
        }

        if (coyoteTimer > 0)
        {
            CanJump = true;
        }
    }
}
