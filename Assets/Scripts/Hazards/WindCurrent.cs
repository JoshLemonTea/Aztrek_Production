using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindCurrent : MonoBehaviour
{
    private Player _player;

    [SerializeField]
    private float _windSpeed = 5f;

    private bool _isActive;

    [SerializeField]
    private bool _startsActive;

    [SerializeField]
    private float _timeActive = 4f;

    [SerializeField]
    private float _timeDisabled = 2.5f;

    private float _timer;

    private Renderer _renderer;

    private Color _originalColor;

    [SerializeField]
    private GameObject _windVFX;

    [SerializeField]
    private ParticleSystem _particleSystem;

    private void OnEnable()
    {
        _player = FindObjectOfType<Player>();
        _renderer = transform.GetChild(0).GetComponent<Renderer>();
        //_originalColor = _renderer.material.color;

        if (_startsActive)
        {
            _isActive = true;
        }
    }

    [System.Obsolete]
    private void Update()
    {
        if (_isActive)
        {
            _timer += Time.deltaTime;
            if(_timer > _timeActive)
            {
                _renderer.material.color = Color.green;
                _timer = 0f;
                _isActive = false;
                //_windVFX.gameObject.SetActive(false);
                _particleSystem.playbackSpeed = 1f;
                _particleSystem.startSpeed = 10f;
                _particleSystem.maxParticles = 15;
            }
        }
        else
        {
            _timer += Time.deltaTime;
            if(_timer > _timeDisabled)
            {
                _renderer.material.color = _originalColor;
                _timer = 0f;
                _isActive = true;
                //_windVFX.gameObject.SetActive(true);
                _particleSystem.playbackSpeed = 2f;
                _particleSystem.startSpeed = 50f;
                _particleSystem.maxParticles = 20;
            }
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (_isActive && other.CompareTag("Player"))
        {
            Vector3 movement = transform.right * _windSpeed;
            _player.Move(_player.Movement + movement);
        }
    }
}
