using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RespawnManager : MonoBehaviour
{
    private Transform _player;

    public Transform ActiveCheckpoint;

    private Transform _cameraTarget;

    private HeartPiece[] _heartPieces;

    public AudioSource AudioSource;
    [SerializeField]
    public AudioClip RespawnSound;

    private void OnEnable()
    {
        _player = FindObjectOfType<Player>().transform;
        _cameraTarget = GameObject.Find("CameraTarget").transform;

        _heartPieces = FindObjectsOfType<HeartPiece>();

        AudioSource = GetComponent<AudioSource>();
    }

    public void Respawn()
    {
        Vector3 offset = new Vector3(0f, 0f, 0.5f);
        _player.position = ActiveCheckpoint.position - offset; 
        _cameraTarget.position = ActiveCheckpoint.position;
        _player.GetComponent<Player>().Movement = Vector3.zero;
        _player.GetComponent<Health>().SetHealth(2);
        _player.GetComponent<Health>().ResetHealthPieceCount();
        foreach (HeartPiece h in _heartPieces)
        {
            h.gameObject.SetActive(true);
        }

        AudioSource.PlayOneShot(RespawnSound, 0.8f);
    }
}