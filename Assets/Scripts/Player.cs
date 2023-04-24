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

    private float JumpForce { get { return Mathf.Sqrt(-2f * GravityValue * JumpHeight); } }

    [SerializeField] private float _acceleration;
    public float Acceleration { get => _acceleration; set => _acceleration = value; }

    [SerializeField] private float _decceleration;
    public float Deceleration { get => _decceleration; set => _decceleration = value; }

    private Vector3 _movement;

    public bool IsGrounded { get { return _characterController.isGrounded; } }

    public bool IsGoingUp { get { return _movement.y > 0f; } }

    public void OnEnter()
    {
        _characterController = GetComponent<CharacterController>();
        GravityValue = Physics.gravity.y;

        transform.position = Vector3.zero;
    }

    public void Move(Vector3 moveInput)
    {
        _movement.x = Mathf.MoveTowards(_movement.x, moveInput.x * MoveSpeed, Acceleration * Time.deltaTime);
        _movement.z = Mathf.MoveTowards(_movement.z, moveInput.y * MoveSpeed, Deceleration * Time.deltaTime);
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
        _movement.y = JumpForce;
    }

    public void FaceForward(Vector2 moveInput)
    {
        if(moveInput != Vector2.zero)
        transform.forward = new Vector3(_movement.x, 0f, _movement.z);
    }

    public void AdjustYMovement(float yMovement)
    {
        _movement.y = yMovement;
    }

}
