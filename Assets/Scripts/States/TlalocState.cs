using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TlalocState : PlayerState
{
    private GameObject _cloudGhost;

    private GameObject _cloud;

    private int _abilityPressCount;

    public TlalocState(PlayerStateMachine playerStateMachine, InputManager inputManager, Player player) : base(playerStateMachine, inputManager, player)
    {
        _cloudGhost = GameObject.Find("CloudGhost");
        _cloudGhost.SetActive(false);
    }

    public override void OnEnter()
    {
        Debug.Log("Entered Tlaloc State");

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
            if (InputManager.HasPressedF)
            {
                if (_abilityPressCount == 0)
                {
                    ShowCloudGhost();
                    _abilityPressCount++;
                }

                if (_abilityPressCount > 0)
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
        Object.Instantiate(_cloud, _cloudGhost.transform.position, _cloudGhost.transform.rotation);
    }
}
