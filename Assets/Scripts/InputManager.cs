using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    private PlayerInput _playerInput;

    public Vector2 MoveInput { get; private set; }

    public bool HasPressedSpace { get; private set; }

    public bool IsHoldingSpace { get; private set; }

    public bool HasPressedF { get; private set; }

    private void OnEnable()
    {
        _playerInput = new PlayerInput();
        _playerInput.Enable();

        _playerInput.Player.Jump.performed += OnPressedJump;
        _playerInput.Player.Jump.canceled += OnReleasedJump;
   
        _playerInput.Player.F.performed += OnPressedF;
        _playerInput.Player.F.canceled += OnReleasedF;
    }

    private void OnReleasedF(InputAction.CallbackContext obj)
    {
        HasPressedF = false;
    }

    private void OnPressedF(InputAction.CallbackContext obj)
    {
        HasPressedF = true;
    }

    private void OnPressedJump(InputAction.CallbackContext obj)
    {
        HasPressedSpace = true;
    }

    private void OnReleasedJump(InputAction.CallbackContext obj)
    {
        HasPressedSpace = false;
    }
    public void OnUpdate()
    {
        MoveInput = _playerInput.Player.Movement.ReadValue<Vector2>();
    }

    private void OnDisable()
    {
        _playerInput.Player.Jump.performed -= OnPressedJump;
        _playerInput.Player.Jump.canceled -= OnReleasedJump;
        _playerInput.Player.F.performed -= OnPressedF;
        _playerInput.Player.F.canceled -= OnReleasedF;

        _playerInput.Disable();
    }
}
