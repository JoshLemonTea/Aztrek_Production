using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireTrap : MonoBehaviour
{
    //[SerializeField] private GameObject _fireGFX; // old
    [SerializeField] private ParticleSystem _fireParticleSystem;

    [SerializeField] private ParticleSystem _smokeParticleSystem;

    [SerializeField] private float _timeActive = 4f;

    [SerializeField] private float _timeDisabled = 2.5f;

    [SerializeField] private int _damage = 1;

    [SerializeField] private float _timeInvulnerable = 2f;

    private BoxCollider _collider;

    private float _timer;

    private bool _isActive;

    private Renderer _renderer;

    private Color _originalColor;

    private AudioSource _audioSource;
    private AudioClip _fireSound;

    private void OnEnable()
    {
        _renderer = transform.GetChild(0).GetComponent<Renderer>();
        _originalColor = _renderer.material.color;
        _collider = GetComponent<BoxCollider>();

        _audioSource = GetComponent<AudioSource>();
        _fireSound = _audioSource.clip;
    }


    private void Update()
    {
        if (_isActive)
        {
            _timer += Time.deltaTime;
            if (_timer > _timeActive)
            {
                _renderer.material.color = Color.green;
                _timer = 0f;
                _isActive = false;

                _collider.enabled = false;

                _fireParticleSystem.playbackSpeed = 1f;
                _fireParticleSystem.startSpeed = 1f;
                _fireParticleSystem.maxParticles = 0;

                //_smokeParticleSystem.playbackSpeed = 0.5f;
                //_smokeParticleSystem.startSpeed = 1f;
                //_smokeParticleSystem.maxParticles = 30;
            }
        }
        else
        {
            _timer += Time.deltaTime;
            if (_timer > _timeDisabled)
            {
                _renderer.material.color = _originalColor;
                _timer = 0f;
                _isActive = true;

                _collider.enabled = true;

                _fireParticleSystem.playbackSpeed = 1f;
                _fireParticleSystem.startSpeed = 4.5f;
                _fireParticleSystem.maxParticles = 30;

                //_smokeParticleSystem.playbackSpeed = 1f;
                //_smokeParticleSystem.startSpeed = 5f;
                //_smokeParticleSystem.maxParticles = 150;

                SoundPitchRandomizer.PlaySoundWithRandomPitch(_audioSource, _fireSound, 0.7f, 0.2f);
            }
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.TryGetComponent(out Health health))
        {
            health.TakeDamage(_damage);
            health.MakeInvulnerable(_timeInvulnerable);
        }
    }
}
