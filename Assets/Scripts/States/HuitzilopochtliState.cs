using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering;

public class HuitzilopochtliState : PlayerState
{
    public HuitzilopochtliState(PlayerStateMachine playerStateMachine, InputManager inputManager, Player player) : base(playerStateMachine, inputManager, player)
    {
        _audioSource = player.GetComponent<AudioSource>();
        _whipSound = Resources.Load<AudioClip>("Whip");
    }
    private bool _canStartGrapple;

    private float _grappleTimer;

    private float _grappleDelayTimer;

    private float _grappleDelayTreshold = 0.5f;

    private Vector3 _grappleDirection;

    private Vector3[] _lineRendererPositions;

    private AudioSource _audioSource;

    private AudioClip _whipSound;

    private float _lavaHeightBoost;

    public override void OnEnter()
    {
        Debug.Log("Entered Huitzilopochtli State");

        State = GodState.Huitzilopochtli;
        base.OnEnter();

        InputManager.Controls.Player.F.performed += OnPressedF;
    }

    private void OnPressedF(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        if (Player.ActiveGrapplePoint != null && !Player.IsGrappling)
        {
            _grappleDirection = Player.ActiveGrapplePoint.position - Player.transform.position;

            if (Player.ActiveGrapplePoint.GetComponent<GrapplePoint>().IsAboveLava)
            {
                _lavaHeightBoost = Player.LavaHeightBoost;
            }
            else
            {
                _lavaHeightBoost = 0f;
            }

            _audioSource.PlayOneShot(_whipSound);

            if (Mathf.Abs(_grappleDirection.x) > Mathf.Abs(_grappleDirection.z))
            {
                _grappleDirection.z = 0f;
            }
            else
            {
                _grappleDirection.x = 0f;
            }

            _grappleDirection.y = 0f;
            _grappleDirection.Normalize();

            if (Player.IsGrounded)
            {
                Player.Jump();

                _canStartGrapple = true;
            }
            else 
            {
                Player.LineRenderer.enabled = true;

                Player.IsGrappling = true;
            }
        }
    }

    public override void OnUpdate()
    {
        if (_canStartGrapple)
        {

            _grappleDelayTimer += Time.deltaTime;
            if (_grappleDelayTimer > _grappleDelayTreshold)
            {
                Player.LineRenderer.enabled = true;

                _grappleDelayTimer = 0f;
                _canStartGrapple = false;

                Player.IsGrappling = true;
            }
        }

        if (Player.IsGrappling)
        {           
            DrawWhip();

            Vector3 moveInput = _grappleDirection;
            float heightChange = -Mathf.Sin(_grappleTimer * 4) * Player.GrappleAmplitude + _lavaHeightBoost;
            moveInput.y = heightChange + 0.1f;



            Player.GrappleMove(moveInput * Player.GrappleSpeed * Time.deltaTime);
            Player.transform.forward = moveInput;

            _grappleTimer += Time.deltaTime;
            if (_grappleTimer > Player.GrappleDuration)
            {
                Player.LineRenderer.enabled = false;

                Player.Move(Player.transform.forward);

                _grappleTimer = 0f;
                Player.IsGrappling = false;

                Player.transform.forward = _grappleDirection;

                _lavaHeightBoost = 0f;
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
            _lineRendererPositions = new Vector3[] { Player.ActiveGrapplePoint.position, 
                (Player.transform.position + Player.transform.up + Player.transform.right) };
            Player.LineRenderer.SetPositions(_lineRendererPositions);
        }
    }

    public override void OnExit()
    {
        base.OnExit();

        InputManager.Controls.Player.F.performed -= OnPressedF;
    }
}
