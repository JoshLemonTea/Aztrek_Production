using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeartPiece : MonoBehaviour
{
    private Health _health;

    private void OnEnable()
    {
        _health =FindObjectOfType<Health>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Health health))
        {
            health.IncrementHearthPieceCount();
            gameObject.SetActive(false);
        }
    }
}
