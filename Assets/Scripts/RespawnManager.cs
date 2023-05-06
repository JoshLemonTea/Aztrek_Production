using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RespawnManager : MonoBehaviour
{
    private Transform _player;

    public Transform ActiveCheckpoint;

    private Transform _cameraTarget;


    private void OnEnable()
    {
        _player = FindObjectOfType<Player>().transform;
        _cameraTarget = GameObject.Find("CameraTarget").transform;
    }

    public void Respawn()
    {
        _player.position = ActiveCheckpoint.position;
        _cameraTarget.position = ActiveCheckpoint.position;
    }
}
