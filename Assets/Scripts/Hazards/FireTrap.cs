using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireTrap : MonoBehaviour
{
    [SerializeField]
    private GameObject _fireGFX;

    [SerializeField]
    private float _timeActive = 4f;

    [SerializeField]
    private float _timeDisabled = 2.5f;

    [SerializeField]
    private int _damage = 1;

    [SerializeField]
    private float _timeInvulnerable = 2f;

    private BoxCollider _collider;

    private float _timer;

    private bool _isActive;

    private Renderer _renderer;

    private Color _originalColor;

    private AudioSource _audioSource;

    private void OnEnable()
    {
        _renderer = transform.GetChild(0).GetComponent<Renderer>();
        _originalColor = _renderer.material.color;
        _collider = GetComponent<BoxCollider>();

        _audioSource = GetComponent<AudioSource>();
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
                _fireGFX.SetActive(false);
                _collider.enabled = false;
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
                _fireGFX.SetActive(true);
                _collider.enabled = true;
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
