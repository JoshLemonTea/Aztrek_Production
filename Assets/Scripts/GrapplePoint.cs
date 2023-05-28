using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GrapplePoint : MonoBehaviour
{
    private Player _player;

    [SerializeField]
    private bool _isAboveLava;

    [SerializeField]
    private GameObject grappleRangeVFX;

    public bool IsAboveLava { get => _isAboveLava; private set => _isAboveLava = value; }

    private void OnEnable()
    {
        _player = FindObjectOfType<Player>();
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player") && !_player.IsGrappling)
        {
            _player.ActiveGrapplePoint = transform;
            grappleRangeVFX.SetActive(true);        
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            _player.ActiveGrapplePoint = null;
            grappleRangeVFX.SetActive(false);
        }
    }
}
