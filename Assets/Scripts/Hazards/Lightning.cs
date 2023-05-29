using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lightning : MonoBehaviour
{
    [SerializeField]
    private CapsuleCollider _collider;

    [SerializeField]
    private GameObject _strikeGFX;

    [SerializeField]
    private float _strikeDelay = 1.5f;

    private float _timer;

    private float _lifeTime = 1f;


    [SerializeField]
    private int _damage = 1;

    [SerializeField]
    private float _timeInvulnerableOnHit = 2f;

    private Gameloop _gameloop;

    private PlayerStateMachine _playerStateMachine;

    private void OnEnable()
    {
        _gameloop = FindObjectOfType<Gameloop>();
        _playerStateMachine = _gameloop.SetPlayerStateMachine();
    }

    private void Update()
    {
        _timer += Time.deltaTime;
        if(_timer > _strikeDelay)
        {
            _strikeGFX.SetActive(true);
            _collider.enabled = true;

            if(_timer > _strikeDelay + _lifeTime)
            {
                Destroy(gameObject);
            }
        }
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Health health))
        {
            if(_playerStateMachine.CurrentState.State == GodState.Tlaloc)
            {
                _playerStateMachine.TlalocState.AddCharge();
            }
            else
            {
                health.TakeDamage(_damage);
                health.MakeInvulnerable(_timeInvulnerableOnHit);
            }
        }
    }

    private void OnBecameInvisible()
    {
        enabled = false;
    }

    private void OnBecameVisible()
    {
        enabled = true;
    }
}
