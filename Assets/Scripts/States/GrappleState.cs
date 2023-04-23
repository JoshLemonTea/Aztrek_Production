using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GrappleState : PlayerState
{
    public GrappleState(PlayerStateMachine playerStateMachine, InputManager inputManager, Player player) : base(playerStateMachine, inputManager, player)
    {
    }

    private LayerMask _grappleLayer;

    private Vector3 _grappleDirection;

    private float _grappleForce = 3f;

    private bool _isGrappling;

    private float _grappleDuration;

    private float _grappleDurationTreshold = 1f;

    public override void OnEnter()
    {
        base.OnEnter();

        _grappleLayer = LayerMask.GetMask("GrappleLayer");

        Debug.Log("Enter Grapple State");
    }

    public override void OnUpdate()
    {
        // Delete later: test code for moving to other state
        if (InputManager.HasPressedTab)
        PlayerStateMachine.GoTo(PlayerStateMachine.HoverState);

        if (InputManager.HasPressedF)
        {
            Vector3 heightOffset = new Vector3(0f, 1f, 0f);
            Vector3 rayOrigin = Player.transform.position + heightOffset;

            float rayDistance = 20f;

            if (Physics.Raycast(rayOrigin, Player.transform.forward, out RaycastHit hit, rayDistance, _grappleLayer))
            {
                _isGrappling = true;
            }
        }

        if (_isGrappling)
        {
            Vector2 moveInput = new Vector2(Player.transform.forward.x, Player.transform.forward.z);
            Player.Move(moveInput * _grappleForce);

            _grappleDuration += Time.deltaTime;
            if(_grappleDuration > _grappleDurationTreshold)
            {
                _grappleDuration = 0f;
                _isGrappling = false;
            }
        }
        else
        {
            base.OnUpdate();
        }



    }

    public override void OnExit()
    {
        base.OnExit();
    }
}
