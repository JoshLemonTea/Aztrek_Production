using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RespawnManager : MonoBehaviour
{
    private Player _player;

    public Transform ActiveCheckpoint;

    private Transform _cameraTarget;

    private HeartPiece[] _heartPieces;

    public AudioSource AudioSource;
    [SerializeField]
    public AudioClip RespawnSound;

    private void OnEnable()
    {
        _player = FindObjectOfType<Player>();
        _cameraTarget = GameObject.Find("CameraTarget").transform;

        _heartPieces = FindObjectsOfType<HeartPiece>();

        AudioSource = GetComponent<AudioSource>();
    }

    public void Respawn()
    {
        Vector3 offset = new Vector3(0f, 0f, 0.5f);
        _player.transform.position = ActiveCheckpoint.position - offset; 
        _cameraTarget.position = ActiveCheckpoint.position;
        _player.Movement = Vector3.zero;
        _player.GetComponent<Health>().SetHealth(2);
        _player.GetComponent<Health>().ResetHealthPieceCount();

        _player.IsGrappling = false;
        _player.ActiveGrapplePoint = null;

        _player.ResetBody();
        _player.ResetHead();

        foreach (HeartPiece h in _heartPieces)
        {
            h.gameObject.SetActive(true);
        }

        AudioSource.PlayOneShot(RespawnSound, 0.8f);
    }
}