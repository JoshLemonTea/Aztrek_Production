using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomTileRotate : MonoBehaviour
{
    void Start()
    {
        this.transform.rotation = new Quaternion(transform.rotation.x, Random.rotation.y,transform.rotation.z,transform.rotation.w);
    }
}