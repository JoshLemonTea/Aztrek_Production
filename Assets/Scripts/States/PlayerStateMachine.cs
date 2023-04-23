using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStateMachine
{
    public PlayerState CurrentState { get; private set; }

    public DefaultState DefaultState { get; private set; }

    public GrappleState GrappleState { get; private set; }

    public HoverState HoverState { get; private set; }

    public JaguarState JaguarState { get; private set; }


    public void InitializeStates(InputManager inputManager, Player player)
    {
        DefaultState = new DefaultState(this, inputManager, player);
        GrappleState = new GrappleState(this, inputManager, player);
        HoverState = new HoverState(this, inputManager, player);
        JaguarState = new JaguarState(this, inputManager, player);
    }

    public void SetInitialState(PlayerState state)
    {
        CurrentState = state;
        CurrentState.OnEnter();
    }

    public void GoTo(PlayerState state)
    {
        CurrentState.OnExit();
        CurrentState = state;
        CurrentState.OnEnter();
    }
}
