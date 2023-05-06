using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class UILookAtCamera : MonoBehaviour
{
    private Transform _target;

    private Transform _camera;

    public TextMeshProUGUI TMPUGUI;

    private Vector3 _targetRotation;

    private void OnEnable()
    {
        _target = GameObject.Find("CameraTarget").transform;
        _camera = Camera.main.transform;
    }

    private void LateUpdate()
    {
        Vector3 forward = -(_camera.position - transform.position).normalized;
        _targetRotation = Quaternion.LookRotation(forward).eulerAngles;
        _targetRotation.y = _target.rotation.eulerAngles.y;

        transform.rotation = Quaternion.Euler(_targetRotation);
    }
}
