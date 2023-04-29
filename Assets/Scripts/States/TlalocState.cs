using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TlalocState : PlayerState
{
    public TlalocState(PlayerStateMachine playerStateMachine, InputManager inputManager, Player player) : base(playerStateMachine, inputManager, player)
    {
    }

    public override void OnEnter()
    {
        Debug.Log("Entered Tlaloc State");

        base.OnEnter();
    }

    public override void OnUpdate()
    {
        base.OnUpdate();
    }

    public override void OnExit()
    {
        base.OnExit();
    }

}
