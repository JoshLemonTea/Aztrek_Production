using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Altar : MonoBehaviour
{
    [SerializeField]
    private GodState _god;

    private PlayerStateMachine _playerStateMachine;

    [SerializeField]
    private UILookAtCamera _UI;

    private InputManager _inputManager;

    private bool _isWithinRange;

    private void Start()
    {
        _inputManager = FindObjectOfType<InputManager>();
        _UI.enabled = false;
        _inputManager.Controls.Player.Tab.performed += OnPressedTab;
    }

    private void OnDisable()
    {
        _inputManager.Controls.Player.Tab.performed -= OnPressedTab;
    }

    private void OnPressedTab(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        if (_isWithinRange)
        {
            if (_god == GodState.Tlaloc)
            {
                _playerStateMachine.GoTo(_playerStateMachine.TlalocState);
            }

            if (_god == GodState.Quetzalcoatl)
            {
                _playerStateMachine.GoTo(_playerStateMachine.QuetzalcoatlState);
            }

            if (_god == GodState.Huitzilopochtli)
            {
                _playerStateMachine.GoTo(_playerStateMachine.HuiztilopochtliState);
            }

            _UI.gameObject.SetActive(false);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            _UI.gameObject.SetActive(true);

            _UI.TMPUGUI.text = "Press TAB to change into " + _god;

            _isWithinRange = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            _UI.gameObject.SetActive(false);

            _isWithinRange = false;
        }
    }

    public void SetStateMachine(PlayerStateMachine stateMachine)
    {
        _playerStateMachine = stateMachine;
    }
}
