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
        Vector2 moveInput = InputManager.MoveInput;

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
}
