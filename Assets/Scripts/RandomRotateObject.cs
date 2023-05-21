using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomRotateObject : MonoBehaviour
{
    private void Awake()
    {
        // random rotation in y as
        gameObject.transform.rotation = new Quaternion(transform.rotation.x, Random.rotation.y, transform.rotation.z, transform.rotation.w);
    }
}