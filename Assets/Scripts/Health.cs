using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    private Renderer _playerRenderer;

    private Color _originalColor;

    private float _timeInvulnerable;

    private float _timer = 0f;

    private bool _canTakeDamage;

    [SerializeField]
    private int _maxHealth = 3;

    private int _currentHealth = 3;

    private RespawnManager _respawnManager;

    private void OnEnable()
    {
        _playerRenderer = FindObjectOfType<Player>().transform.GetChild(0).GetComponent<Renderer>();
        _originalColor = _playerRenderer.material.color;
        _respawnManager = FindObjectOfType<RespawnManager>();
    }

    private void LimitCurrentHealth()
    {
        if(_currentHealth > _maxHealth)
        {
            _currentHealth = _maxHealth;
        }
        if(_currentHealth <= 0)
        {
            _respawnManager.Respawn();
            _currentHealth = _maxHealth;
        }
    }

    public void Heal(int amount)
    {
        _currentHealth += amount;
        LimitCurrentHealth();
    }

    public void TakeDamage(int damage)
    {
        _currentHealth -= damage;
        LimitCurrentHealth();
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
}
