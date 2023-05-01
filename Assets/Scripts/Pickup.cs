using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickup : MonoBehaviour
{
    [SerializeField]
    private float _amplitude = 1f;

    [SerializeField]
    private float _frequency = 2f;

    [SerializeField]
    private float _phase = 0f;

    [SerializeField]
    private float _rotationSpeed = 90f;

    private float _initialHeight;

    private void OnEnable()
    {
        _initialHeight = transform.position.y;
    }

    private void Update()
    {
        float y =  _initialHeight + Mathf.Sin(Time.time * _frequency + _phase) * _amplitude;

        transform.position = new Vector3(transform.position.x, y, transform.position.z);

        transform.Rotate(Vector3.up, _rotationSpeed * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            gameObject.SetActive(false);
        }
    }
}
