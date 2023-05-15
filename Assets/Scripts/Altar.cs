using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.ProBuilder.Shapes;
using UnityEngine.Video;

public class Altar : MonoBehaviour
{
    [SerializeField]
    private GodState _god;

    private PlayerStateMachine _playerStateMachine;

    [SerializeField]
    private UILookAtCamera _UI;

    private InputManager _inputManager;

    private bool _isWithinRange;
    private AudioSource _audioSource;

    [SerializeField]
    private bool _playTutorialVideo;

    [SerializeField]
    private VideoPlayer _videoPlayer;

    [SerializeField]
    private GameObject _videoUI;

    private void Start()
    {
        _inputManager = FindObjectOfType<InputManager>();
        _UI.enabled = false;
        _inputManager.Controls.Player.Tab.performed += OnPressedTab;
        _audioSource = GetComponent<AudioSource>();

        _videoPlayer.loopPointReached += OnVideoCompleted;
        _videoUI.SetActive(false);
    }

    private void OnVideoCompleted(VideoPlayer source)
    {
        _videoUI.SetActive(false);
        Time.timeScale = 1;
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

            _audioSource.Play();

            _UI.enabled = false;

            _UI.TMPUGUI.text = "";

            if (_playTutorialVideo)
            {
                PlayTutorialVideo();
            }

        }
    }

    private void PlayTutorialVideo()
    {
        _videoUI.SetActive(true);
        _videoPlayer.Play();
        Time.timeScale = 0;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            _UI.enabled = true;

            //_UI.TMPUGUI.text = "Press TAB to change into " + _god;
            _UI.TMPUGUI.text = "Presiona TAB para interactuar con " + _god + ".";
            
            _isWithinRange = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            _UI.enabled = false;

            _UI.TMPUGUI.text = "";

            _isWithinRange = false;
        }
    }

    public void SetStateMachine(PlayerStateMachine stateMachine)
    {
        _playerStateMachine = stateMachine;
    }
}
