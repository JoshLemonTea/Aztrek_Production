using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JaguarState : PlayerState
{
    public JaguarState(PlayerStateMachine playerStateMachine, InputManager inputManager, Player player) : base(playerStateMachine, inputManager, player)
    {
    }

    public override void OnEnter()
    {
        base.OnEnter();
        Debug.Log("Enter Jaguar State");
    }

    public override void OnUpdate()
    {
        base.OnUpdate();
        // Delete later: test code for moving to other state
        if (InputManager.HasPressedTab)
        PlayerStateMachine.GoTo(PlayerStateMachine.DefaultState);
    }

    public override void OnExit()
    {
        base.OnExit();
    }
}
