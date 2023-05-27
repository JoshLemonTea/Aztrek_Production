using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    private RespawnManager _respawnManager;
    public GameObject resurrectionVFX;

    private AudioSource _audioSource;
    private AudioClip _respawnSound;

    private void OnEnable()
    {
        _respawnManager = FindObjectOfType<RespawnManager>();
        _audioSource = _respawnManager.AudioSource;
        _respawnSound = _respawnManager.RespawnSound;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            _respawnManager.ActiveCheckpoint = this.transform;
            resurrectionVFX.SetActive(true);
            _audioSource.PlayOneShot(_respawnSound, 0.5f);
        }
    }
}