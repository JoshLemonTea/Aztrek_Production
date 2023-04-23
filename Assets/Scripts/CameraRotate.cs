using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CameraRotate : MonoBehaviour
{
    [SerializeField] private float rotationDuration;
    [SerializeField] private int rotationIncrements;
    private Vector3 targetRotation = Vector3.zero;
    private Vector3 currentRotation;
    private Vector3 velocity = Vector3.zero;

    public void OnRotateClockwise(InputAction.CallbackContext context)
    {
        if (context.performed == true)
        {
            targetRotation.y += rotationIncrements;
        }
    }

    public void OnRotateCounterClockwise(InputAction.CallbackContext context)
    {
        if (context.performed == true)
        {
            targetRotation.y -= rotationIncrements;
        }
    }

    // Update is called once per frame
    void Update()
    {
        StartRotation();
    }

    private void StartRotation()
    {
        currentRotation = Vector3.SmoothDamp(currentRotation, targetRotation, ref velocity, rotationDuration);
        Quaternion rotationQuaternion = Quaternion.Euler(currentRotation);
        transform.rotation = rotationQuaternion;
    }
}
