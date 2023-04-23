using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    [SerializeField]
    private Transform _player;

    [SerializeField]
    private float _moveSpeed = 10f;

    void LateUpdate()
    {
        transform.position = Vector3.MoveTowards(transform.position, _player.position, _moveSpeed * Time.deltaTime);
    }
}
