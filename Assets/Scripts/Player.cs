using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Player : MonoBehaviour
{
    private CharacterController _characterController;

    public float MoveSpeed { get; set; }

    public float GravityValue { get; set; }

    public float JumpHeight { get; set; }

    private float JumpForce { get { return Mathf.Sqrt(-2f * GravityValue * JumpHeight); } }

    public float Acceleration { get; set; }

    public float Deceleration { get; set; }
    
    private Vector3 _movement;

    public bool IsGrounded { get { return _characterController.isGrounded; } }

    public bool IsGoingUp { get { return _movement.y > 0f; } }

    public void OnEnter()
    {
        _characterController = GetComponent<CharacterController>();
        MoveSpeed = 6f;
        GravityValue = Physics.gravity.y;
        JumpHeight = 3f;
        Acceleration = 20f;
        Deceleration = 20f;

        transform.position = Vector3.zero;
    }

    public void Move(Vector2 moveInput)
    {
        _movement.x = Mathf.MoveTowards(_movement.x, moveInput.x * MoveSpeed, Acceleration * Time.deltaTime);
        _movement.z = Mathf.MoveTowards(_movement.z, moveInput.y * MoveSpeed, Deceleration * Time.deltaTime);
        _characterController.Move(_movement * Time.deltaTime);
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
        if (IsGrounded)
        {
            _movement.y = JumpForce;
        }
    }

    public void FaceForward(Vector2 moveInput)
    {
        if(moveInput != Vector2.zero)
        transform.forward = new Vector3(moveInput.x, 0f, moveInput.y);
    }
}
