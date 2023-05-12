using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManageActivation : MonoBehaviour
{
    [SerializeField]
    private GameObject _UI;

    private void OnEnable()
    {
        _UI.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            _UI.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            _UI.SetActive(false);
        }
    }
}
