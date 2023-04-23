using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    public Vector2 MoveInput { get; private set; }

    public bool HasPressedSpace { get; private set; }

    public bool IsHoldingSpace { get; private set; }

    public bool HasPressedF { get; private set; }

    public bool HasPressedTab { get; private set; }

    //Custom events that will be fired by the new InputSystem
    public void OnMove(InputAction.CallbackContext context)
    {
        MoveInput = context.ReadValue<Vector2>();
    }

    public void OnJump(InputAction.CallbackContext context)
    {
        if (context.ReadValue<float>() == 0)
        {
            HasPressedSpace = false;
        }
        else
        {
            HasPressedSpace = true;
        }
        HasPressedSpace = context.action.triggered;
    }

    public void OnPressF(InputAction.CallbackContext context)
    {
        if (context.ReadValue<float>() == 0)
        {
            HasPressedF = false;
        }
        else
        {
            HasPressedF = true;
        }
        HasPressedF = context.action.triggered;
    }

    public void OnPressTab(InputAction.CallbackContext context)
    {
        if (context.ReadValue<float>() == 0)
        {
            HasPressedTab = false;
        }
        else
        {
            HasPressedTab = true;
        }
        HasPressedTab = context.action.triggered;
    }

    public void OnUpdate()
    {
        //Old Input

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
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            HasPressedTab = true;
        }
        else
        {
            HasPressedTab = false;
        }

        if (Input.GetKeyDown(KeyCode.T)) // Add check for teleport key
        {
            HasPressedTeleportKey = true;
        }
        else
        {
            HasPressedTeleportKey = false;
        }
    }
}
