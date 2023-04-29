using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Altar : MonoBehaviour
{
    [SerializeField]
    private GodState _god;

    private PlayerStateMachine _playerStateMachine;

    public void SetStateMachine(PlayerStateMachine stateMachine)
    {
        _playerStateMachine = stateMachine;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (_god == GodState.Tlaloc)
            {
                _playerStateMachine.GoTo(_playerStateMachine.TlalocState);
            }
            if (_god == GodState.Quetzalcoatl)
            {
                _playerStateMachine.GoTo(_playerStateMachine.QuetzalcoatlState);

            }
            if(_god == GodState.Huitzilopochtli)
            {
                _playerStateMachine.GoTo(_playerStateMachine.HuiztilopochtliState);
            }
        }
    }
}
