using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Water : MonoBehaviour
{
    private RespawnManager _respawnManager;

    private void OnEnable()
    {
        _respawnManager = FindObjectOfType<RespawnManager>();
    }

    private void OnTriggerEnter(Collider other)
    {
        _respawnManager.Respawn();
    }
}
