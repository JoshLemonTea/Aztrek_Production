using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    private Renderer _playerRenderer;

    private Color _originalColor;

    [SerializeField]
    private float _timeInvulnerable = 2f;

    private float _timer = 0f;

    private bool _canTakeDamage;

    private void OnEnable()
    {
        _playerRenderer = FindObjectOfType<Player>().transform.GetChild(0).GetComponent<Renderer>();
        _originalColor = _playerRenderer.material.color;
    }

    public void TakeDamage(int damage)
    {
        // Add code to make player take damage
    }

    public void MakeInvulnerable(float duration)
    {
        _timeInvulnerable = duration;
        _canTakeDamage = false;
        _playerRenderer.material.color = Color.red;
    }

    private void Update()
    {
        LimitInvulnerability();
    }

    private void LimitInvulnerability()
    {
        if (!_canTakeDamage)
        {
            _timer += Time.deltaTime;
            if (_timer > _timeInvulnerable)
            {
                _timer = 0f;
                _canTakeDamage = true;
                _playerRenderer.material.color = _originalColor;
            }
        }
    }

    internal void MakeInvulnerable(object timeInvulnerable)
    {
        throw new NotImplementedException();
    }

    internal void TakeDamage(object damage)
    {
        throw new NotImplementedException();
    }
}
