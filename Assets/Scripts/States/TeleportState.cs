using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportationState : PlayerState
{
    private Vector3 teleportDestination; // The destination for teleportation
    private float teleportDistance; // The distance to teleport horizontally

    public TeleportationState(PlayerStateMachine playerStateMachine, InputManager inputManager, Player player, Vector3 destination, float distance) : base(playerStateMachine, inputManager, player)
    {
        teleportDestination = destination;
        teleportDistance = distance;
    }

    public override void OnEnter()
    {
        base.OnEnter();
        Debug.Log("Entered Teleportation State");
        TeleportPlayer();
    }

    public override void OnUpdate()
    {
        // Leave the state immediately after teleporting
        PlayerStateMachine.GoTo(PlayerStateMachine.DefaultState);
    }

    public override void OnExit()
    {
        base.OnExit();
    }

    private void TeleportPlayer()
    {
        // Calculate the teleportation destination based on the player's current position and facing direction
        Vector3 facingDirection = Player.transform.forward;
        Vector3 horizontalDestination = Player.transform.position + (facingDirection * teleportDistance);
        teleportDestination = new Vector3(horizontalDestination.x, Player.transform.position.y, horizontalDestination.z);

        // Teleport the player to the calculated destination
        Player.transform.position = teleportDestination;
        Debug.Log("Player Teleported to: " + teleportDestination);
    }
}
