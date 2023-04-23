using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JaguarState : PlayerState
{
    public JaguarState(PlayerStateMachine playerStateMachine, InputManager inputManager, Player player) : base(playerStateMachine, inputManager, player)
    {
    }

    private bool _isWallRunning;

    private float _wallRunTimeTreshold = 2f;

    private float _wallRunTimer;

    private LayerMask _wallLayer;

    private LayerMask _groundLayer;

    private RaycastHit _leftHitwall;

    private RaycastHit _rightHitwall;

    private bool _hasHitLeftWall;

    private bool _hasHitRightWall;

    private float _wallRunSpeed = 6f;

    private bool _isJaguar;

    private bool _canWallrunAgain = true;

    public override void OnEnter()
    {
        _wallLayer = LayerMask.GetMask("Wall");
        _groundLayer = LayerMask.GetMask("Ground");

        base.OnEnter();
        Debug.Log("Enter Jaguar State");
    }

    public override void OnUpdate()
    {
        // Delete later: test code for moving to other state
        if (InputManager.HasPressedTab)
        PlayerStateMachine.GoTo(PlayerStateMachine.DefaultState);

        if (InputManager.HasPressedF)
        {
            _isJaguar = !_isJaguar;
            Player.MoveSpeed = 12f;
        }

        if (_isJaguar)
        {
            CheckForWall();

            if ((_hasHitLeftWall || _hasHitRightWall) && Mathf.Abs(InputManager.MoveInput.y) > 0f && IsAboveGround() && _canWallrunAgain)
            {
                _isWallRunning = true;
                _canWallrunAgain = false;
            }

            if (_isWallRunning)
            {
                Vector3 wallNormal = _hasHitRightWall ? _rightHitwall.normal : _leftHitwall.normal;

                Vector3 wallForward = Vector3.Cross(wallNormal, Vector3.up);

                if((Player.transform.forward - wallForward).magnitude > (Player.transform.forward - -wallForward).magnitude)
                {
                    wallForward = -wallForward;
                }

                Player.WallRun(wallForward * _wallRunSpeed);

                _wallRunTimer += Time.deltaTime;
                if(_wallRunTimer > _wallRunTimeTreshold || !(Mathf.Abs(InputManager.MoveInput.y) > 0f))
                {
                    _wallRunTimer = 0f;
                    _isWallRunning = false;
                    Player.AdjustYMovement(0f);
                }

                if (InputManager.HasPressedSpace)
                {
                    Player.Jump();
                    _wallRunTimer = 0f;
                    _isWallRunning = false;
                }
            }
            else
            {
                base.OnUpdate();
            }
        }
        else
        {
            Player.MoveSpeed = 6f;
            base.OnUpdate();
        }

        if (Player.IsGrounded)
        {
            _canWallrunAgain = true;
        }

    }

    public override void OnExit()
    {
        base.OnExit();
    }

    private void CheckForWall()
    {
        float distance = 1f;
        _hasHitLeftWall = Physics.Raycast(Player.transform.position, -Player.transform.right, out _leftHitwall, distance, _wallLayer);
        _hasHitRightWall = Physics.Raycast(Player.transform.position, Player.transform.right, out _rightHitwall, distance, _wallLayer);
    }

    private bool IsAboveGround()
    {
        float distance = 1f;
        return !Physics.Raycast(Player.transform.position, Vector3.down, distance, _groundLayer);
    }
}
