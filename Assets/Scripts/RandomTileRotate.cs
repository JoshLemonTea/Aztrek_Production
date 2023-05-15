using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomRotateObject : MonoBehaviour
{
    void Start()
    {
        this.transform.rotation = new Quaternion(transform.rotation.x, Random.rotation.y,transform.rotation.z,transform.rotation.w);
    }
}