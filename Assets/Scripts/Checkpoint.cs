using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    private RespawnManager _respawnManager;
    public GameObject resurrectionVFX;

    private void OnEnable()
    {
        
        _respawnManager = FindObjectOfType<RespawnManager>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            _respawnManager.ActiveCheckpoint = this.transform;
            resurrectionVFX.SetActive(true);
        }
    }
}
