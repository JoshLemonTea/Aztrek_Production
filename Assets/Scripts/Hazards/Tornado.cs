using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tornado : MonoBehaviour
{
    private Player _player;

    [SerializeField]
    private float _windSpeed = 5f;

    [SerializeField]
    private float _timeActive = 4f;

    [SerializeField]
    private float _timeDisabled = 2.5f;

    private float _timer;

    [SerializeField]
    private float _timeInvulnerable = 2f;

    [SerializeField]
    private int _damage = 1;

    private void OnEnable()
    {
        _player = FindObjectOfType<Player>();
    }

    private void OnTriggerEnter(Collider other)
    {

        if (other.TryGetComponent(out Health health))
        {
            health.TakeDamage(_damage);
            health.MakeInvulnerable(_timeInvulnerable);
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Vector3 movement = transform.up * _windSpeed;
            _player.AddMovement(movement);
        }
    }
}
