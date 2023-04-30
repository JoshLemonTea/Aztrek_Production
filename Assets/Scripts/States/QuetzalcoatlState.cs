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

    public override void OnEnter()
    {
        Debug.Log("Entered Quetzalcoatl State");
        State = GodState.Quetzalcoatl;
        base.OnEnter();
    }

    public override void OnUpdate()
    {
        base.OnUpdate();
      
        if (InputManager.HasPressedF)
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
