using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gameloop : MonoBehaviour
{
    private InputManager _input;

    private Player _player;

    [HideInInspector]public PlayerStateMachine _playerSM;

    private Altar[] _altars;

    private HazardInDistanceDisabler _hazardInDistanceDisabler;
    private float _hazardsCheckCounter;


    private void OnEnable()
    {
        _input = FindObjectOfType<InputManager>();

        _player = FindObjectOfType<Player>();
        _player.OnEnter();

        _playerSM = new PlayerStateMachine();
        _playerSM.InitializeStates(_input, _player);
        _playerSM.SetInitialState(_playerSM.DefaultState);

        _hazardInDistanceDisabler = new HazardInDistanceDisabler();
        _hazardInDistanceDisabler.FireTraps = FindObjectsOfType<FireTrap>();
        _hazardInDistanceDisabler.LightningSpawners = FindObjectsOfType<LightningSpawner>();
        _hazardInDistanceDisabler.Tornados = FindObjectsOfType<Tornado>();
        _hazardInDistanceDisabler.WindCurrents = FindObjectsOfType<WindCurrent>();
        _hazardInDistanceDisabler.Player = FindObjectOfType<Player>().transform;

        _altars = FindObjectsOfType<Altar>();
        foreach(Altar altar in _altars)
        {
            altar.SetStateMachine(_playerSM);
        }
    }

    private void Update()
    {
        _input.OnUpdate();

        _playerSM.CurrentState.OnUpdate();

        if(_hazardsCheckCounter <= 0)
        {
            _hazardInDistanceDisabler.DisableHazardsInDistance();
            _hazardsCheckCounter = 1.5f;
        }
        else
        {
            _hazardsCheckCounter -= Time.deltaTime;
        }
    }

    public PlayerStateMachine SetPlayerStateMachine()
    {
        return _playerSM;
    }
}
