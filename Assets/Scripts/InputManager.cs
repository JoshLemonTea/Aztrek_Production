using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    public PlayerInput Controls;

    public Vector2 MoveInput { get; private set; }

    public bool HasPressedSpace { get; private set; }

    public bool IsHoldingSpace { get; private set; }

    public bool HasPressedF { get; private set; }

    private void OnEnable()
    {
        Controls = new PlayerInput();
        Controls.Enable();

        Controls.Player.Jump.performed += OnPressedJump;
        Controls.Player.Jump.canceled += OnReleasedJump;
   
        Controls.Player.F.performed += OnPressedF;
        Controls.Player.F.canceled += OnReleasedF;
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
        MoveInput = Controls.Player.Movement.ReadValue<Vector2>();
    }

    private void OnDisable()
    {
        Controls.Player.Jump.performed -= OnPressedJump;
        Controls.Player.Jump.canceled -= OnReleasedJump;
        Controls.Player.F.performed -= OnPressedF;
        Controls.Player.F.canceled -= OnReleasedF;

        Controls.Disable();
    }
}
