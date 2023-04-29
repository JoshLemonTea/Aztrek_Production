using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Altar : MonoBehaviour
{
    [SerializeField]
    private God _god;

    private PlayerStateMachine _playerStateMachine;

    public void SetStateMachine(PlayerStateMachine stateMachine)
    {
        _playerStateMachine = stateMachine;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (_god == God.Tlaloc)
            {
                _playerStateMachine.GoTo(_playerStateMachine.JaguarState);
            }
            if (_god == God.Quetzalcoatl)
            {
                _playerStateMachine.GoTo(_playerStateMachine.HoverState);

            }
            if(_god == God.Huitzilopochtli)
            {
                _playerStateMachine.GoTo(_playerStateMachine.GrappleState);
            }
        }
    }
}
