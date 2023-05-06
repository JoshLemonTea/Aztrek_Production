using System;
using UnityEngine;

public class QuetzalcoatlState : PlayerState
{
    public QuetzalcoatlState(PlayerStateMachine playerStateMachine, InputManager inputManager, Player player) : base(playerStateMachine, inputManager, player)
    {
    }

    public override void OnEnter()
    {
        Debug.Log("Entered Quetzalcoatl State");
        State = GodState.Quetzalcoatl;
        base.OnEnter();

        InputManager.Controls.Player.F.performed += OnPressedF;
    }

    private void OnPressedF(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        Player.IsHovering = !Player.IsHovering;

        Player.MoveSpeed = Player.HoverMoveSpeed;
        Player.Acceleration = Player.HoverModeAcceleration;
        Player.Deceleration = Player.HoverModeDeceleration;
    }

    public override void OnUpdate()
    {
        base.OnUpdate();

        if (Player.IsHovering)
        {
            if (InputManager.HasPressedSpace && !Player.IsGoingUp)
            {
                Player.GravityValue = Player.HoverGravityValue;
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
        InputManager.Controls.Player.F.performed -= OnPressedF;

        Player.IsHovering = false;
        ResetDefaultPlayerValues();

        base.OnExit();
    }
}
