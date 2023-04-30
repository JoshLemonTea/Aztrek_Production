using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PlayerState
{
    public PlayerStateMachine PlayerStateMachine { get; }

    public InputManager InputManager { get; private set; }

    public Player Player { get; }

    public GodState State { get; set; }

    private float _originalMoveSpeed;

    private float _originalAcceleration;

    private float _originalDeceleration;

    private float _originalAirAcceleration;

    private float _originalAirDeceleration;

    public PlayerState(PlayerStateMachine playerStateMachine, InputManager inputManager, Player player)
    {
        PlayerStateMachine = playerStateMachine;
        InputManager = inputManager;
        Player = player;

        _originalMoveSpeed = Player.MoveSpeed;
        _originalAcceleration = Player.Acceleration;
        _originalDeceleration = Player.Deceleration;
        _originalAirAcceleration = Player.AirAcceleration;
        _originalAirDeceleration = Player.AirDeceleration;
    }

    public virtual void OnEnter() { }

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

    public virtual void OnExit()
    {
        ResetDefaultPlayerValues();
    }

    protected void ResetDefaultPlayerValues()
    {
        Player.MoveSpeed = _originalMoveSpeed;
        Player.Acceleration = _originalAcceleration;
        Player.Deceleration = _originalDeceleration;
        Player.AirAcceleration = _originalAirAcceleration;
        Player.AirDeceleration = _originalAirDeceleration;
        Player.GravityValue = Physics.gravity.y * 3f;
    }
}
