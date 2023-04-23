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

        if (InputManager.HasPressedSpace)
        {
            Player.Jump();
        }
    }

    public virtual void OnExit() { }
}
