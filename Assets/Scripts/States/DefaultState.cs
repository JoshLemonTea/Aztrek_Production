using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefaultState : PlayerState
{
    private float teleportDistance = 10f; // Define the teleportation distance here

    public DefaultState(PlayerStateMachine playerStateMachine, InputManager inputManager, Player player) : base(playerStateMachine, inputManager, player)
    {
    }

    public override void OnEnter()
    {
        base.OnEnter();
        Debug.Log("Entered Default State");
    }

    public override void OnUpdate()
    {
        base.OnUpdate();

        // Delete later: test code for moving to other state
        if (InputManager.HasPressedTab)
            PlayerStateMachine.GoTo(PlayerStateMachine.GrappleState);

        // Add teleportation logic
        if (InputManager.HasPressedTeleportKey) // Replace with the appropriate input key for teleportation
        {
            // Calculate the teleportation destination
            Vector3 teleportDestination = Player.transform.position + (Player.transform.forward * teleportDistance); // Use the teleportDistance defined in this script

            // Create a new TeleportationState and transition to it
            TeleportationState teleportationState = new TeleportationState(PlayerStateMachine, InputManager, Player, teleportDestination, teleportDistance);
            PlayerStateMachine.GoTo(teleportationState);
        }
    }

    public override void OnExit()
    {
        base.OnExit();
    }
}
