using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TlalocState : PlayerState
{
    private GameObject _tlalocGodUI;
    private GameObject _cloudGhost;

    private GameObject _cloud;

    private GameObject _previousCloud;

    private bool _hasCharge = false;

    private int _abilityPressCount;

    private AudioSource _audioSource;
    private AudioClip _cloudPlaceSound;

    private Image _tlalocCloudUI;

    public TlalocState(PlayerStateMachine playerStateMachine, InputManager inputManager, Player player) : base(playerStateMachine, inputManager, player)
    {
        _tlalocGodUI = GameObject.Find("TlalocGodUI");
        _tlalocGodUI.SetActive(false);
        _cloudGhost = GameObject.Find("CloudGhost");

        _cloud = Resources.Load<GameObject>("Cloud");

        _audioSource = player.GetComponent<AudioSource>();
        _cloudPlaceSound = Resources.Load<AudioClip>("Put Cloud");
        
        GameObject tlalocCloudUI = GameObject.Find("TlalocCloudUI");
        if(tlalocCloudUI != null)
        {
            _tlalocCloudUI = tlalocCloudUI.GetComponent<Image>();
            _tlalocCloudUI.enabled = false;
        }

        _cloudGhost.SetActive(false);
    }

    public override void OnEnter()
    {
        Debug.Log("Entered Tlaloc State");
        State = GodState.Tlaloc;
        base.OnEnter();

        _tlalocGodUI.SetActive(true);

        InputManager.Controls.Player.F.performed += OnPressedF;
    }

    private void OnPressedF(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        TriggerAbility();
    }

    public override void OnUpdate()
    {
        base.OnUpdate();

        if (!Player.IsGrounded)
        {
            HideCloudGhost();
            _abilityPressCount = 0;
        }
        else if (Player.CoyoteTime < 0.2f)
        {
            Player.CoyoteTime += Time.deltaTime / 4;
            if (Player.CoyoteTime > 0.2f)
                Player.CoyoteTime = 0.2f;
        }
    }

    public override void OnExit()
    {
        InputManager.Controls.Player.F.performed -= OnPressedF;

        _tlalocGodUI.SetActive(false);

        base.OnExit();

        _abilityPressCount = 0;
        HideCloudGhost();
    }

    private void TriggerAbility()
    {
        if (Player.IsGrounded)
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
                Player.CoyoteTime = 0;
                _abilityPressCount = 0;
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
        _audioSource.PlayOneShot(_cloudPlaceSound);

        if (_hasCharge)
        {
            Object.Instantiate(_cloud, _cloudGhost.transform.position, _cloudGhost.transform.rotation);
            _hasCharge = false;
            _tlalocCloudUI.enabled = false;
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

        if (_tlalocCloudUI != null)
            _tlalocCloudUI.enabled = true;
    }
}