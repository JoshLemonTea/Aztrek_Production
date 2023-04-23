using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PlayerState
{
    public PlayerStateMachine PlayerStateMachine { get; }

    public InputManager InputManager { get; private set; }

    public Player Player { get; }

    public PlayerState(PlayerStateMachine playerStateMachine, InputManager inputManager, Player player)
    {
        PlayerStateMachine = playerStateMachine;
        InputManager = inputManager;
        Player = player;
    }

    public virtual void OnEnter()
    {
    }

    public virtual void OnUpdate()
    {
        Vector2 moveInput = RelativeMovementVector();
        //Vector2 moveInput = InputManager.MoveInput;

        Player.FaceForward(moveInput);

        Player.Move(moveInput);

        Player.ApplyGravity();

        if (InputManager.HasPressedSpace && Player.IsGrounded)
        {
            Player.Jump();
        }

        if (InputManager.HasPressedTeleportKey) // Add check for HasPressedTeleportKey
        {
            // Add teleportation logic here
            // e.g. create a new TeleportationState and transition to it
            // TeleportationState teleportationState = new TeleportationState(PlayerStateMachine, InputManager, Player, teleportDestination, teleportDistance);
            // PlayerStateMachine.GoTo(teleportationState);
        }
    }

    public virtual void OnExit() { }


    //Changes the movement input to be relative to the camera (Not sure where else to put this)
    private Vector3 RelativeMovementVector()
    {
        float horizontalAxis = InputManager.MoveInput.x;
        float verticalAxis = InputManager.MoveInput.y;

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
}
