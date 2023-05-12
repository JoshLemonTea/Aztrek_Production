using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tornado : MonoBehaviour
{
    private Player _player;

    [SerializeField]
    private float _windSpeed = 5f;

    [SerializeField]
    private float _timeInvulnerable = 2f;

    [SerializeField]
    private int _damage = 1;

    [SerializeField]
    private float _moveSpeed = 2f;

    [SerializeField]
    private Transform[] _targets;

    private int _currentIndex = 0;

    private PlayerStateMachine _playerStateMachine;

    private void Start()
    {
        _player = FindObjectOfType<Player>();

        _playerStateMachine = FindObjectOfType<Gameloop>().SetPlayerStateMachine();

        foreach(Transform target in _targets)
        {
            target.parent = null;
        }
    }

    private void Update()
    {
        if (Vector3.Distance(transform.position, _targets[_currentIndex].position) < 0.1f)
        {
            _currentIndex++;
            if (_currentIndex > _targets.Length - 1)
            {
                _currentIndex = 0;
            }
        }

        transform.position = Vector3.MoveTowards(transform.position, _targets[_currentIndex].position, _moveSpeed * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Health health) && _playerStateMachine.CurrentState.State != GodState.Quetzalcoatl)
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

    private void OnBecameVisible()
    {
        enabled = true;
    }

    private void OnBecameInvisible()
    {
        enabled = false;
    }
}
