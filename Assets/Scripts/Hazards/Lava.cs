using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lava : MonoBehaviour
{
    [SerializeField]
    private int _damage = 1;

    [SerializeField]
    private float _timeInvulnerable = 1f;

    private void OnTriggerStay(Collider other)
    {
        if (other.TryGetComponent(out Player player))
        {
            player.GetComponent<Health>().TakeDamage(_damage);
            player.GetComponent<Health>().MakeInvulnerable(_timeInvulnerable);
            player.JumpMovement();
        }
    }
}
