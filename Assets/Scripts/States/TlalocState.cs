using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TlalocState : PlayerState
{
    private GameObject _cloudGhost;

    private GameObject _cloud;

    private GameObject _previousCloud;

    private bool _hasCharge = true;

    private int _abilityPressCount;

    private bool _previousFramePressF;
    private bool _currentFramePressF;

    private bool FPressed
    {
        get
        {
            _previousFramePressF = _currentFramePressF;
            _currentFramePressF = InputManager.HasPressedF;

            if (_previousFramePressF == false && _currentFramePressF == true)
                return true;
            else
                return false;
        }
    }

    public TlalocState(PlayerStateMachine playerStateMachine, InputManager inputManager, Player player) : base(playerStateMachine, inputManager, player)
    {
        _cloudGhost = GameObject.Find("CloudGhost");
        _cloudGhost.SetActive(false);
    }

    public override void OnEnter()
    {
        Debug.Log("Entered Tlaloc State");
        State = GodState.Tlaloc;
        base.OnEnter();

        _cloud = Resources.Load<GameObject>("Cloud");
    }

    public override void OnUpdate()
    {
        base.OnUpdate();

        TriggerAbility();
    }

    public override void OnExit()
    {
        base.OnExit();
    }

    private void TriggerAbility()
    {
        if (Player.IsGrounded)
        {
            if (FPressed)
            {
                if (_abilityPressCount == 0)
                {
                    ShowCloudGhost();
                    _abilityPressCount++;
                }
                else if (_abilityPressCount > 0)
                {
                    HideCloudGhost();
                    PlaceCloud();
                    _abilityPressCount = 0;
                }
            }
        }
        else
        {
            HideCloudGhost();
            _abilityPressCount = 0;
        }
    }

    private void HideCloudGhost()
    {
        _cloudGhost.SetActive(false);
    }

    private void ShowCloudGhost()
    {
        _cloudGhost.SetActive(true);
    }

    private void PlaceCloud()
    {
        if (_hasCharge)
        {
            Object.Instantiate(_cloud, _cloudGhost.transform.position, _cloudGhost.transform.rotation);
            _hasCharge = false;
        }
        else
        {
            GameObject.Destroy(_previousCloud);

            _previousCloud = Object.Instantiate(_cloud, _cloudGhost.transform.position, _cloudGhost.transform.rotation);
        }
    }

    public void AddCharge()
    {
        _hasCharge = true;
    }
}
