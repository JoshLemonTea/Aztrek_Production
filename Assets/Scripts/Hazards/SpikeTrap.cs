using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikeTrap : MonoBehaviour
{
    [SerializeField]
    private int _damage = 1;

    [SerializeField]
    private float _timeInvulnerableOnHit = 2f;

    private void OnTriggerEnter(Collider other)
    {
        if(other.TryGetComponent(out Health health))
        {
            health.TakeDamage(_damage);
            health.MakeInvulnerable(_timeInvulnerableOnHit);
        }
    }
}
