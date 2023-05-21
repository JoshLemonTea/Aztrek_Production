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
        Vector3 offset = new Vector3(0f, 0f, 0.5f);
        _player.position = ActiveCheckpoint.position - offset; 
        _cameraTarget.position = ActiveCheckpoint.position;
    }
}