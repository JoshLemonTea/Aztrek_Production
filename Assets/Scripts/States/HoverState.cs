using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class HoverState : PlayerState
{
    public HoverState(PlayerStateMachine playerStateMachine, InputManager inputManager, Player player) : base(playerStateMachine, inputManager, player)
    {
    }

    private bool _isHovering;

    private float _hoverGravityValue = Physics.gravity.y / 4f;

    private float _hoverMoveSpeed = 15f;

    private float _hoverModeAcceleration = 15f;

    private float _hoverModeDeceleration = 15f;

    public override void OnEnter()
    {
        Debug.Log("Entered Hover State");
    }

    public override void OnUpdate()
    {
        base.OnUpdate();

        // Reset default values and move to next state by pressing Tab
        if (InputManager.HasPressedTab)
        PlayerStateMachine.GoTo(PlayerStateMachine.JaguarState);
        
        // (De)activate hovering mode
        if (InputManager.HasPressedF)
        {
            _isHovering = !_isHovering;
        }

        // When hovering: use different movespeed & (when holding space & moving down) use different gravity value 
        if (_isHovering)
        {
            Player.MoveSpeed = _hoverMoveSpeed;
            Player.Acceleration = _hoverModeAcceleration;
            Player.Deceleration = _hoverModeDeceleration;


            if (InputManager.IsHoldingSpace && !Player.IsGoingUp)
            {
                Player.GravityValue = _hoverGravityValue;
            }
            else
            {
                Player.GravityValue = Physics.gravity.y;
            }
        }
        else
        {
            Player.GravityValue = Physics.gravity.y;
            Player.MoveSpeed = 6f;
            Player.Acceleration = _hoverModeAcceleration;
            Player.Deceleration = _hoverModeDeceleration;
        }
    }

    public override void OnExit()
    {
        _isHovering = false;
        Player.GravityValue = Physics.gravity.y;
        Player.MoveSpeed = 6f;
        Player.Acceleration = _hoverModeAcceleration;
        Player.Deceleration = _hoverModeDeceleration;
    }
}
