using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    public Vector2 MoveInput { get; private set; }

    public bool HasPressedSpace { get; private set; }

    public bool IsHoldingSpace { get; private set; }

    public bool HasPressedF { get; private set; }

    public bool HasPressedTab { get; private set; }

    public void OnUpdate()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        MoveInput = new Vector2 (horizontalInput, verticalInput).normalized;

        if (Input.GetKeyDown(KeyCode.Space))
        {
            HasPressedSpace = true;
        }
        else
        {
            HasPressedSpace = false;
        }

        if (Input.GetKey(KeyCode.Space))
        {
            IsHoldingSpace = true;
        }
        else
        {
            IsHoldingSpace = false;
        }

        if (Input.GetKeyDown(KeyCode.F))
        {
            HasPressedF = true;
        }
        else
        {
            HasPressedF = false;
        }

        if (Input.GetKeyDown(KeyCode.Tab))
        {
            HasPressedTab = true;
        }
        else
        {
            HasPressedTab = false;
        }
    }
}
