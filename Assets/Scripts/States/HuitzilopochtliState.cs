using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class HuitzilopochtliState : PlayerState
{
    public HuitzilopochtliState(PlayerStateMachine playerStateMachine, InputManager inputManager, Player player) : base(playerStateMachine, inputManager, player)
    {
    }

    private LayerMask _grappleLayer;

    private float _grappleForce = 2f;

    private bool _isGrappling;

    private float _grappleDuration;

    private float _grappleDurationTreshold = 1f;

    public override void OnEnter()
    {
        Debug.Log("Entered Huitzilopochtli State");
        State = GodState.Huitzilopochtli;
        base.OnEnter();

        _grappleLayer = LayerMask.GetMask("GrappleLayer");
    }

    public override void OnUpdate()
    {
        if (InputManager.HasPressedF && !_isGrappling)
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
