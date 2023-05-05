using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GrapplePoint : MonoBehaviour
{
    public bool IsVisible { get; private set; }

    private Player _player;

    private Renderer _renderer;

    private Color _originalColor;

    private void OnEnable()
    {
        _player = FindObjectOfType<Player>();
        _renderer = transform.GetComponent<Renderer>();
        _originalColor = _renderer.material.color;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (IsVisible)
            {
                _player.ActiveGrapplePoint = transform;
                _renderer.material.color = Color.green;
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            _player.ActiveGrapplePoint = null;
            _renderer.material.color = _originalColor;
        }
    }

    private void OnBecameVisible()
    {
        IsVisible = true;
    }

    private void OnBecameInvisible()
    {
        IsVisible = false;
    }
}
