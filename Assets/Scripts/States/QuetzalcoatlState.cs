using System;
using UnityEngine;

public class QuetzalcoatlState : PlayerState
{
    private GameObject _quetzalGodUI;

    public QuetzalcoatlState(PlayerStateMachine playerStateMachine, InputManager inputManager, Player player) : base(playerStateMachine, inputManager, player)
    {
        _quetzalGodUI = GameObject.Find("QuetzalGodUI");
        _quetzalGodUI.SetActive(false);
    }

    public override void OnEnter()
    {
        Debug.Log("Entered Quetzalcoatl State");
        State = GodState.Quetzalcoatl;
        base.OnEnter();

        _quetzalGodUI.SetActive(true);

        InputManager.Controls.Player.F.performed += OnPressedF;
    }

    private void OnPressedF(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        Player.IsHovering = !Player.IsHovering;

        if (Player.IsHovering)
        {
            Player.HoverVFX.SetActive(true);
        }
        else
        {
            Player.HoverVFX.SetActive(false);

        }


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

        _quetzalGodUI.SetActive(false);

        Player.HoverVFX.SetActive(false);

        base.OnExit();
    }
}
