using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    [SerializeField] private PlayerInput _playerInput;
    public Vector2 MoveInput { get; private set; }

    public bool HasPressedSpace { get; private set; }

    public bool IsHoldingSpace { get; private set; }

    public bool HasPressedF { get; private set; }

    public bool HasPressedTab { get; private set; }

    public bool HasPressedTeleportKey { get; private set; }

    public void OnPressF(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            HasPressedF = true;
        }
        else
        {
            HasPressedF = false;
        }
    }

    public void OnPressTab(InputAction.CallbackContext context)
    {
        HasPressedTab = true;
    }

    public void OnPressTeleportKey(InputAction.CallbackContext context)
    {
        if (context.ReadValue<float>() == 0)
        {
            HasPressedTeleportKey = false;
        }
        else
        {
            HasPressedTeleportKey = true;
        }
        HasPressedTeleportKey = context.action.triggered;
    }

    private void OnEnable()
    {
        _playerInput = new PlayerInput();
        _playerInput.Enable();

        //Jump
        _playerInput.Player.Jump.performed += OnPressedJump;
        _playerInput.Player.Jump.canceled += OnReleasedJump;

        //Tab
        _playerInput.Player.Tab.performed += OnPressedJump;
        _playerInput.Player.Tab.canceled += OnReleasedJump;
    }

    private void OnPressedJump(InputAction.CallbackContext obj)
    {
        HasPressedSpace = true;
    }

    private void OnReleasedJump(InputAction.CallbackContext obj)
    {
        HasPressedSpace = false;
    }

    private void OnPressedTab(InputAction.CallbackContext obj)
    {
        HasPressedSpace = true;
    }
    private void OnReleasedTab(InputAction.CallbackContext obj)
    {
        HasPressedSpace = false;
    }

    public void OnUpdate()
    {
        //Movement
        MoveInput = _playerInput.Player.Movement.ReadValue<Vector2>();

        //Old input

        //float horizontalInput = Input.GetAxis("Horizontal");
        //float verticalInput = Input.GetAxis("Vertical");

        //MoveInput = new Vector2 (horizontalInput, verticalInput).normalized;

        //if (Input.GetKeyDown(KeyCode.Space))
        //{
        //    HasPressedSpace = true;
        //}
        //else
        //{
        //    HasPressedSpace = false;
        //}

        //if (Input.GetKey(KeyCode.Space))
        //{
        //    IsHoldingSpace = true;
        //}
        //else
        //{
        //    IsHoldingSpace = false;
        //}

        //if (Input.GetKeyDown(KeyCode.F))
        //{
        //    HasPressedF = true;
        //}
        //else
        //{
        //    HasPressedF = false;
        //}

        //if (Input.GetKeyDown(KeyCode.Tab))
        //{
        //    HasPressedTab = true;
        //}
        //else
        //{
        //    HasPressedTab = false;
        //}
    }

    private void OnDisable()
    {
        _playerInput.Player.Jump.performed -= OnPressedJump;
        _playerInput.Player.Jump.canceled -= OnReleasedJump;

        _playerInput.Disable();
    }
}
