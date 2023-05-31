using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeartPiece : MonoBehaviour
{
    private AudioSource _audioSource;
    [SerializeField]
    private AudioClip _heartPickupSound;

    private float _counter = 0;
    private bool _collected = false;

    private void OnEnable()
    {
        _audioSource = GetComponent<AudioSource>();
    }

    private void Update()
    {
        if (_collected)
        {
            _counter += Time.deltaTime;
            if (_counter >= 0.5f)
            {
                _counter = 0;
                _collected = false;
                GetComponentInChildren<MeshRenderer>().enabled = true;
                GetComponent<Collider>().enabled = true;
                gameObject.SetActive(false);
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Health health))
        {
            if (health.CurrentHealth < health._maxHealth) // this is so that you can't pick up heart pieces when full health
            {
                SoundPitchRandomizer.PlaySoundWithRandomPitch(_audioSource, _heartPickupSound, 1f, 0.2f);
                health.IncrementHearthPieceCount();
                GetComponentInChildren<MeshRenderer>().enabled = false;
                GetComponent<Collider>().enabled = false;
                _collected = true;
            }
        }
    }
}
