using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class Health : MonoBehaviour
{
    private Renderer _playerRenderer;

    private Color _originalColor;

    private float _timeInvulnerable;

    private float _timer = 0f;

    public bool CanTakeDamage { get; private set; }

    [SerializeField] public int _maxHealth = 3;

    [SerializeField] private IngameUI ingameUI;

    [HideInInspector] public int _currentHealth = 3;

    private RespawnManager _respawnManager;

    private AudioSource _audioSource;
    [SerializeField] private AudioClip _hitSound;

    // screenshake
    [SerializeField] private ScreenShake shake;

    public int HeartPieceCount { get; private set; }

    [SerializeField]
    private int RequiredHearthPieces = 10;

    private void OnEnable()
    {
        //_playerRenderer = FindObjectOfType<Player>().transform.GetChild(0).GetComponent<Renderer>();
        //_originalColor = _playerRenderer.material.GetColor("_BaseColor");
        _respawnManager = FindObjectOfType<RespawnManager>();
        _audioSource = GetComponent<AudioSource>();
    }

    public void IncrementHearthPieceCount()
    {
        HeartPieceCount++;
        if (HeartPieceCount >= RequiredHearthPieces)
        {
            ResetHealthPieceCount();
            Heal(1);
        }
    }

    public void ResetHealthPieceCount()
    {
        HeartPieceCount = 0;
    }

    private void LimitCurrentHealth()
    {
        if (_currentHealth > _maxHealth)
        {
            _currentHealth = _maxHealth;
        }
        if (_currentHealth <= 0)
        {
            _respawnManager.Respawn();
            //_playerRenderer.material.SetColor("_BaseColor", _originalColor);
        }
    }

    public void Heal(int amount)
    {
        _currentHealth += amount;
        LimitCurrentHealth();
    }

    public void TakeDamage(int damage)
    {
        if (CanTakeDamage)
        {
            _currentHealth -= damage;
            SoundPitchRandomizer.PlaySoundWithRandomPitch(_audioSource, _hitSound, 1, 0.25f);
            LimitCurrentHealth();
            shake.ShakeScreen(); // Screen Shake
        }
    }

    public void MakeInvulnerable(float duration)
    {
        if (CanTakeDamage)
        {
            _timeInvulnerable = duration;
            CanTakeDamage = false;
            if (_currentHealth > 0)
            {
                // _playerRenderer.material.SetColor("_BaseColor", Color.red);
            }
            else
            {
                _currentHealth = _maxHealth;
            }
        }
    }

    private void Update()
    {
        LimitInvulnerability();
    }

    private void LimitInvulnerability()
    {
        if (!CanTakeDamage)
        {
            _timer += Time.deltaTime;
            if (_timer >= _timeInvulnerable)
            {
                _timer = 0f;
                CanTakeDamage = true;
                //_playerRenderer.material.SetColor("_BaseColor", _originalColor);
            }
        }
    }
}
