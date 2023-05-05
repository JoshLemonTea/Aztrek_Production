using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering;

public class HuitzilopochtliState : PlayerState
{
    public HuitzilopochtliState(PlayerStateMachine playerStateMachine, InputManager inputManager, Player player) : base(playerStateMachine, inputManager, player)
    {
    }

    private LayerMask _grappleLayer;

    private float _grappleForce = 18f;

    private bool _isGrappling;

    private float _grappleDuration;

    private float _grappleDurationTreshold = 1.2f;

    private Vector3 _grappleDirection;

    private Vector3[] _lineRendererPositions;

    public override void OnEnter()
    {
        Debug.Log("Entered Huitzilopochtli State");
        State = GodState.Huitzilopochtli;
        base.OnEnter();

        _grappleLayer = LayerMask.GetMask("GrappleLayer");
    }

    public override void OnUpdate()
    {
        if (InputManager.HasPressedF && Player.ActiveGrapplePoint != null && !_isGrappling)
        {
            _isGrappling = true;

            _grappleDirection = Player.ActiveGrapplePoint.position - Player.transform.position;
            _grappleDirection.y = 0f;
            _grappleDirection.Normalize();

            Player.Jump();

            Player.LineRenderer.enabled = true;
        }

        if (_isGrappling)
        {
            Debug.Log("Grappling");

            DrawWhip();

            Vector3 moveInput = _grappleDirection;
            float heightChange = -Mathf.Sin(_grappleDuration * 4) * 0.4f;
            moveInput.y = heightChange;
            Player.GrappleMove(moveInput * _grappleForce * Time.deltaTime);

            _grappleDuration += Time.deltaTime;
            if(_grappleDuration > _grappleDurationTreshold)
            {
                _grappleDuration = 0f;
                _isGrappling = false;
                Player.LineRenderer.enabled = false;
            }
        }
        else
        {
            base.OnUpdate();
        }
    }

    private void DrawWhip()
    {
        if(Player.ActiveGrapplePoint != null)
        {
            _lineRendererPositions = new Vector3[] { Player.transform.position, Player.ActiveGrapplePoint.position };
            Player.LineRenderer.SetPositions(_lineRendererPositions);
        }
    }

    public override void OnExit()
    {
        base.OnExit();

        Player.LineRenderer.enabled = false;
    }
}
