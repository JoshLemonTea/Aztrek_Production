using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cloud : MonoBehaviour
{
    [SerializeField] private float _lifeTime = 15f;
    private Collider collider;

    private void Start()
    {
        Collider[] allColliders = GetComponents<Collider>();
        for (int i = 0; i < allColliders.Length; i++)
        {
            if (allColliders[i].isTrigger == false)
            {
                collider = allColliders[i];
            }
        }

        collider.enabled = false;
    }
    private void Update()
    {
        CountDown();
    }

    private void CountDown()
    {
        _lifeTime -= Time.deltaTime;

        if (_lifeTime <= 0)
        {
            Destroy(gameObject);
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<Player>() != null)
        {
            ChangeState(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.GetComponent<Player>() != null)
        {
            ChangeState(false);
        }
    }

    private void ChangeState(bool state)
    {
        collider.enabled = state;
    }
}