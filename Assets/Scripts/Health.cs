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

    [HideInInspector] public int CurrentHealth = 3;

    private RespawnManager _respawnManager;

    private AudioSource _audioSource;
    [SerializeField] private AudioClip _hitSound;

    // screenshake
    //[SerializeField] private ScreenShake shake;

    public int HeartPieceCount { get; private set; }


    public int RequiredHearthPieces = 10;

    public GameObject hitVFX;

    private void OnEnable()
    {
        //_playerRenderer = FindObjectOfType<Player>().transform.GetChild(0).GetComponent<Renderer>();
        //_originalColor = _playerRenderer.material.GetColor("_BaseColor");
        _respawnManager = FindObjectOfType<RespawnManager>();
        _audioSource = GetComponent<AudioSource>();
    }

    public void IncrementHearthPieceCount()
    {
        if (CurrentHealth != _maxHealth)
        {
            HeartPieceCount++;
            if (HeartPieceCount >= RequiredHearthPieces)
            {
                ResetHealthPieceCount();
                Heal(1);
            }
        }

    }

    public void ResetHealthPieceCount()
    {
        HeartPieceCount = 0;
    }

    private void LimitCurrentHealth()
    {
        if (CurrentHealth > _maxHealth)
        {
            CurrentHealth = _maxHealth;
        }
        if (CurrentHealth <= 0)
        {
            _respawnManager.Respawn();
            //_playerRenderer.material.SetColor("_BaseColor", _originalColor);
        }
    }

    public void Heal(int amount)
    {
        CurrentHealth += amount;
        LimitCurrentHealth();
    }

    public void SetHealth(int health)
    {
        CurrentHealth = health;
    }

    public void TakeDamage(int damage)
    {
        if (CanTakeDamage)
        {
            CurrentHealth -= damage;
            SoundPitchRandomizer.PlaySoundWithRandomPitch(_audioSource, _hitSound, 1, 0.2f);
            LimitCurrentHealth();
            //shake.ShakeScreen(); // Screen Shake
            hitVFX.SetActive(true);
        }
    }

    public void MakeInvulnerable(float duration)
    {
        if (CanTakeDamage)
        {
            //hitVFX.SetActive(false);
            _timeInvulnerable = duration;
            CanTakeDamage = false;
            if (CurrentHealth > 0)
            {
                // _playerRenderer.material.SetColor("_BaseColor", Color.red);
            }
            else
            {
                CurrentHealth = _maxHealth;
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
