using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManageActivation : MonoBehaviour
{
    [SerializeField]
    private GameObject _UI;
    [SerializeField]
    private bool _canBeSeenWithAbility = true;
    [SerializeField]
    private bool _canBeSeenWithoutAbility = true;
    private Gameloop _gameLoop;
    private bool playerHasAbility;

    private void OnEnable()
    {
        _UI.SetActive(false);
        _gameLoop = GameObject.Find("Gameloop").GetComponent<Gameloop>();
    }

    private void Update()
    {
        playerHasAbility = (_gameLoop._playerSM.CurrentState != _gameLoop._playerSM.DefaultState);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (_canBeSeenWithAbility && _canBeSeenWithoutAbility)
            {
                _UI.SetActive(true);
            }

            if (_canBeSeenWithAbility && playerHasAbility)
            {
                _UI.SetActive(true);
            }

            if (_canBeSeenWithoutAbility && !playerHasAbility)
            {
                _UI.SetActive(true);
            }
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
