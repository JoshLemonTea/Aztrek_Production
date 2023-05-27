using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TlalocState : PlayerState
{
    private GameObject _tlalocGodUI;
    private GameObject _cloudGhost;
    private GameObject _miniCloudPivot;
    private GameObject _miniCloud;
    private GameObject _miniCloudPowerUp;

    private float _miniCloudRespawnTime;

    private GameObject _cloud;

    private GameObject _previousCloud;

    private bool _hasCharge = false;

    private int _abilityPressCount;

    private AudioSource _audioSource;
    private AudioClip _cloudPlaceSound;

    public TlalocState(PlayerStateMachine playerStateMachine, InputManager inputManager, Player player) : base(playerStateMachine, inputManager, player)
    {
        _tlalocGodUI = GameObject.Find("TlalocGodUI");
        _tlalocGodUI.SetActive(false);
        _cloudGhost = GameObject.Find("CloudGhost");
        _miniCloud = GameObject.Find("MiniCloud");
        _miniCloudPowerUp = GameObject.Find("MiniCloudPowerUp");
        _miniCloudPivot = GameObject.Find("MiniCloudPivot");

        _cloud = Resources.Load<GameObject>("Cloud");

        _audioSource = player.GetComponent<AudioSource>();
        _cloudPlaceSound = Resources.Load<AudioClip>("Put Cloud");

        _cloudGhost.SetActive(false);
        _miniCloudPowerUp.SetActive(false);
        _miniCloud.SetActive(false);
        _miniCloudPivot.SetActive(false);
    }

    public override void OnEnter()
    {
        Debug.Log("Entered Tlaloc State");
        State = GodState.Tlaloc;
        base.OnEnter();

        _tlalocGodUI.SetActive(true);
        _miniCloud.SetActive(true);
        _miniCloudPowerUp.SetActive(false);
        _miniCloudPivot.SetActive(true);

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

        _miniCloudPivot.transform.Rotate(Vector3.up, 220 * Time.deltaTime);

        if (_miniCloudRespawnTime > 0)
            _miniCloudRespawnTime -= Time.deltaTime;
        else
            _miniCloud.SetActive(true);
    }

    public override void OnExit()
    {
        InputManager.Controls.Player.F.performed -= OnPressedF;

        _tlalocGodUI.SetActive(false);
        _miniCloudPowerUp.SetActive(false);
        _miniCloud.SetActive(false);
        _miniCloudPivot.SetActive(false);

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
        SoundPitchRandomizer.PlaySoundWithRandomPitch(_audioSource, _cloudPlaceSound, 1f, 0.3f);

        if (_hasCharge)
        {
            Object.Instantiate(_cloud, _cloudGhost.transform.position, _cloudGhost.transform.rotation);
            _hasCharge = false;
            _miniCloudPowerUp.SetActive(false);
        }
        else
        {
            GameObject.Destroy(_previousCloud);

            _previousCloud = Object.Instantiate(_cloud, _cloudGhost.transform.position, _cloudGhost.transform.rotation);

            _miniCloud.SetActive(false);
            _miniCloudRespawnTime = 15f;
        }
    }

    public void AddCharge()
    {
        _hasCharge = true;

        _miniCloudPowerUp.SetActive(true);
    }
}