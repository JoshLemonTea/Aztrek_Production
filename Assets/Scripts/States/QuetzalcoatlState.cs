using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class QuetzalcoatlState : PlayerState
{
    public QuetzalcoatlState(PlayerStateMachine playerStateMachine, InputManager inputManager, Player player) : base(playerStateMachine, inputManager, player)
    {
    }

    private bool _isHovering;

    private float _hoverGravityValue = Physics.gravity.y / 10f;

    private float _hoverMoveSpeed = 20f;

    private float _hoverModeAcceleration = 50f;

    private float _hoverModeDeceleration = 25f;

    private bool _previousFramePressF;
    private bool _currentFramePressF;

    private bool FPressed
    {
        get
        {
            _previousFramePressF = _currentFramePressF;
            _currentFramePressF = InputManager.HasPressedF;

            if (_previousFramePressF == false && _currentFramePressF == true)
                return true;
            else
                return false;
        }
    }

    public override void OnEnter()
    {
        Debug.Log("Entered Quetzalcoatl State");
        State = GodState.Quetzalcoatl;
        base.OnEnter();
    }

    public override void OnUpdate()
    {
        base.OnUpdate();
      
        if (FPressed)
        {
            _isHovering = !_isHovering;

            Player.MoveSpeed = _hoverMoveSpeed;
            Player.Acceleration = _hoverModeAcceleration;
            Player.Deceleration = _hoverModeDeceleration;
        }

        if (_isHovering)
        {
            if (InputManager.HasPressedSpace && !Player.IsGoingUp)
            {
                Player.GravityValue = _hoverGravityValue;
            }
            else
            {
                Player.GravityValue = Physics.gravity.y * 3f;
            }
        }
        else
        {
            ResetDefaultPlayerValues();
        }
    }

    public override void OnExit()
    {
        _isHovering = false;
        base.OnExit();
    }
}
