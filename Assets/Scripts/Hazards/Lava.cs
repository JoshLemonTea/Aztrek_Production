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
            if (player.GetComponent<Health>().CanTakeDamage && player.GetComponent<Health>()._currentHealth > 1)
                player.JumpMovement(false);
            player.GetComponent<Health>().TakeDamage(_damage);
            player.GetComponent<Health>().MakeInvulnerable(_timeInvulnerable);
        }
    }
}
