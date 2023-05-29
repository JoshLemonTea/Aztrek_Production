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
            if (player.GetComponent<Health>().CanTakeDamage && player.GetComponent<Health>().CurrentHealth > 1)
            {
                player.Movement = (transform.up * player.JumpForce);
            }
            player.GetComponent<Health>().TakeDamage(_damage);
            player.GetComponent<Health>().MakeInvulnerable(_timeInvulnerable);
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
