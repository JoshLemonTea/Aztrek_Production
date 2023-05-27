using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikeTrap : MonoBehaviour
{
    [SerializeField]
    private int _damage = 1;

    [SerializeField]
    private float _timeInvulnerableOnHit = 2f;

    private Player _player;

    private void OnEnable()
    {
        _player = FindObjectOfType<Player>();
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.TryGetComponent(out Health health))
        {
            if (health.CanTakeDamage && health.CurrentHealth > 1)
            {
                _player.JumpMovement(false);
            }
            health.TakeDamage(_damage);
            health.MakeInvulnerable(_timeInvulnerableOnHit);
        }
    }
}