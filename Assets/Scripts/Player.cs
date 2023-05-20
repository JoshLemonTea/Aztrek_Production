using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
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
    [HideInInspector] public Vector3 Movement;

    public bool IsGrounded { get { return _characterController.isGrounded; } }

    public bool IsGoingUp { get { return Movement.y > 0f; } }

    public bool CanJump { get; set; }

    //Added this for the bouncy platform script
    [HideInInspector] public float lastGroundHeight;

    [SerializeField] public float CoyoteTime;
    private float coyoteTimer;

    private AudioSource _audioSource;
    [SerializeField]
    private AudioClip _jumpSound;

    [SerializeField] private Transform _cameraTarget;

    public Transform CameraTarget { get => _cameraTarget; set => _cameraTarget = value; }

    public Transform ActiveGrapplePoint { get; set; }

    public LineRenderer LineRenderer { get; private set; }

    [SerializeField]
    private float _grappleDuration = 1.2f;
    public float GrappleDuration { get => _grappleDuration; set => _grappleDuration = value; }

    [SerializeField]
    private float _grappleAmplitude = 0.3f;
    public float GrappleAmplitude { get => _grappleAmplitude; set => _grappleAmplitude = value; }

    [SerializeField]
    private float _grappleSpeed = 20f;
    public float GrappleSpeed { get => _grappleSpeed; set => _grappleSpeed = value; }

    public bool IsGrappling { get; set; }

    [SerializeField]
    private float _hoverMoveSpeed = 15f;
    public float HoverMoveSpeed { get => _hoverMoveSpeed; set => _hoverMoveSpeed = value; }

    [SerializeField]
    private float _hoverModeAcceleration = 50;
    public float HoverModeAcceleration { get => _hoverModeAcceleration; set => _hoverModeAcceleration = value; }

    [SerializeField]
    private float _hoverModeDeceleration = 20;
    public float HoverModeDeceleration { get => _hoverModeDeceleration; set => _hoverModeDeceleration = value; }

    [SerializeField]
    private float _hoverGravityValue = Physics.gravity.y / 5f;
    public float HoverGravityValue { get => _hoverGravityValue; set => _hoverGravityValue = value; }

    public bool IsHovering { get; set; }

    [SerializeField]
    private float _lavaHeightBoost = 0.5f;

    public float LavaHeightBoost { get => _lavaHeightBoost; private set => _lavaHeightBoost = value; }

    [SerializeField]
    private GameObject _hoverVFX;

    public GameObject HoverVFX { get => _hoverVFX; private set => _hoverVFX = value; }

    [SerializeField]
    private Transform _whipTail;

    [SerializeField]
    private Transform _whipHead;

    [SerializeField]
    private Transform _whipBody;

    [SerializeField]
    private Transform _originalHeadPosition;

    [SerializeField]
    private GameObject _whip;

    public Transform WhipTail { get => _whipTail;}

    public Transform WhipHead { get => _whipHead; }

    public Transform WhipBody { get => _whipBody;}

    public Transform OriginalHeadPosition { get => _originalHeadPosition; }

    public GameObject Whip { get => _whip;}

    public Quaternion OriginalHeadRotation { get; private set; }

    public Quaternion OriginalTailRotation { get; private set; }

    private void Update()
    {
        if (IsGrounded)
        {
            lastGroundHeight = transform.position.y;
        }

        HandleCoyoteTime();
    }

    public void OnEnter()
    {
        _characterController = GetComponent<CharacterController>();

        //Increased Gravity gives snappier jumps
        GravityValue = Physics.gravity.y * 6;

        LineRenderer = GetComponent<LineRenderer>();
        LineRenderer.enabled = false;

        _audioSource = GetComponent<AudioSource>();

        OriginalHeadRotation = WhipHead.localRotation;

        OriginalTailRotation = WhipTail.localRotation;
    }

    public void Move(Vector3 moveInput)
    {
        Vector3 relativeInput = RelativeMovementVector(moveInput);

        if (IsGrounded)
        {
            if(moveInput.magnitude > 0f)
            {
                Movement.x = Mathf.MoveTowards(Movement.x, relativeInput.x * MoveSpeed, Acceleration * Time.deltaTime);
                Movement.z = Mathf.MoveTowards(Movement.z, relativeInput.y * MoveSpeed, Acceleration * Time.deltaTime);
            }
            else
            {
                Movement.x = Mathf.MoveTowards(Movement.x, relativeInput.x * MoveSpeed, Deceleration * Time.deltaTime);
                Movement.z = Mathf.MoveTowards(Movement.z, relativeInput.y * MoveSpeed, Deceleration * Time.deltaTime);
            }
        }
        else
        {
            if(moveInput.magnitude > 0f)
            {


                Movement.x = Mathf.MoveTowards(Movement.x, relativeInput.x * MoveSpeed, AirAcceleration * Time.deltaTime);
                Movement.z = Mathf.MoveTowards(Movement.z, relativeInput.y * MoveSpeed, AirAcceleration * Time.deltaTime);
            }
            else
            {
                Movement.x = Mathf.MoveTowards(Movement.x, relativeInput.x * MoveSpeed, AirDeceleration * Time.deltaTime);
                Movement.z = Mathf.MoveTowards(Movement.z, relativeInput.y * MoveSpeed, AirDeceleration * Time.deltaTime);
            }
        }

        _characterController.Move(Movement * Time.deltaTime);
    }

    public void GrappleMove(Vector3 movement)
    {
        _characterController.Move(movement);
    }

    public void WallRun(Vector3 wallrunMovement)
    {
        _characterController.Move(wallrunMovement * Time.deltaTime);
    }

    public void ApplyGravity()
    {
        if (!IsGrounded)
        {
            Movement.y += GravityValue * Time.deltaTime;
        }
        else
        {
            float defaultDownwardForce = -1f;
            Movement.y = defaultDownwardForce;
        }
    }

    public void Jump()
    {
        if (CoyoteTime == 0)
        {
            CanJump = IsGrounded;
        }
        Debug.Log(IsGrounded);

        if (CanJump)
        {
            JumpMovement(true);
        }
        else
        {
            if (Movement.y > 0.2f)
            {
                _currentAddedJumpForce -= Time.deltaTime * _addedJumpFalloff;
                Movement.y += _currentAddedJumpForce * Time.deltaTime;
                //Debug.Log(_currentAddedJumpForce);
            }
        }

        //Immediately disable Coyote Time
        coyoteTimer = 0;

        GetComponentInChildren<AnimationManager>().OnJump();
    }

    public void JumpMovement(bool mustPlaysound)
    {
        if (mustPlaysound)
            _audioSource.PlayOneShot(_jumpSound, 0.75f);

        Movement.y = JumpForce;
        _currentAddedJumpForce = -GravityValue * _addedJumpForce;
    }

    public void FaceForward(Vector2 moveInput)
    {
        if(moveInput != Vector2.zero)
        transform.forward = new Vector3(Movement.x, 0f, Movement.z);
    }
    
    public void AddMovement(Vector3 movement)
    {
        Movement += movement;
    }

    public void AdjustYMovement(float yMovement)
    {
        Movement.y = yMovement;
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
            CanJump = true;
            coyoteTimer = CoyoteTime;
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