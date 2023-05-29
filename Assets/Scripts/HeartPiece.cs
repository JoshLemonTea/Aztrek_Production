using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeartPiece : MonoBehaviour
{
    private AudioSource _audioSource;
    [SerializeField]
    private AudioClip _heartPickupSound;

    private void OnEnable()
    {
        _audioSource = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Health health))
        {
            SoundPitchRandomizer.PlaySoundWithRandomPitch(_audioSource, _heartPickupSound, 1f, 0.2f);
            health.IncrementHearthPieceCount();
            Destroy(gameObject, 0.3f);
        }
    }
}
