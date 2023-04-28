using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightningSpawner : MonoBehaviour
{
    [SerializeField]
    private GameObject _lightningPrefab;

    [SerializeField]
    private BoxCollider _collider;

    [SerializeField]
    private float _spawnDelay = 3f;

    [SerializeField]
    private int _strikesPerCast = 5;

    private float _timer;

    private bool _canSpawn;

    private Vector3 GetRandomPosition()
    {
        float halfWidth = _collider.size.x / 2f;
        float halfDepth = _collider.size.z / 2f;

        float minX = transform.position.x - halfWidth;
        float maxX = transform.position.x + halfWidth;
        float minZ = transform.position.z - halfDepth;
        float maxZ = transform.position.z + halfDepth;

        Vector3 position = transform.position;
        float randomX = Random.Range(minX, maxX);
        float randomZ = Random.Range(minZ, maxZ);
        position.x = randomX;
        position.z = randomZ;

        return position;
    }

    private void SpawnLightning()
    {    
        Vector3 position = GetRandomPosition();
        Instantiate(_lightningPrefab, position, Quaternion.identity);     
    }


    private void Update()
    {
        if (!_canSpawn)
        {
            _timer += Time.deltaTime;
            if(_timer > _spawnDelay)
            {
                _timer = 0f;
                _canSpawn = true;
            }
        }
        else
        {
            for (int i = 0; i < _strikesPerCast; i++)
            {
                SpawnLightning();
                _canSpawn = false;
            }
        }
    }
}
