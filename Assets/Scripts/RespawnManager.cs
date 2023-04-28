using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RespawnManager : MonoBehaviour
{
    private Transform _player;

    public Transform ActiveCheckpoint;

    private void OnEnable()
    {
        _player = FindObjectOfType<Player>().transform;
    }

    public void Respawn()
    {
        _player.position = ActiveCheckpoint.position;
    }
}
