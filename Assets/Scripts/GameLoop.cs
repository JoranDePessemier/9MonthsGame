using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameLoop : MonoBehaviour
{
    [SerializeField]
    private List<GameSectionView> _sections;

    private StateMachine _gameStateMachine;

    private void Awake()
    {
        _gameStateMachine = new StateMachine();

        for (int i = 0; i < _sections.Count ; i++)
        {
            _gameStateMachine.Register($"GameState{i}", new GameState(_sections[i]));
        }

        _gameStateMachine.InitialState = $"GameState{0}";
    }
}
