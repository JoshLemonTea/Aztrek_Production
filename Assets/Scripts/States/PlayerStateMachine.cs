using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStateMachine
{
    public PlayerState CurrentState { get; private set; }

    public DefaultState DefaultState { get; private set; }

    public HuitzilopochtliState HuiztilopochtliState { get; private set; }

    public QuetzalcoatlState QuetzalcoatlState { get; private set; }

    public TlalocState TlalocState { get; private set; }


    public void InitializeStates(InputManager inputManager, Player player)
    {
        DefaultState = new DefaultState(this, inputManager, player);
        HuiztilopochtliState = new HuitzilopochtliState(this, inputManager, player);
        QuetzalcoatlState = new QuetzalcoatlState(this, inputManager, player);
        TlalocState = new TlalocState(this, inputManager, player);
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
