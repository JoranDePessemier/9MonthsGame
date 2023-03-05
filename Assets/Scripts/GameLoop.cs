using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameLoop : MonoBehaviour
{
    [SerializeField]
    private List<GameSectionView> _sections;

    [SerializeField]
    private MainMenuView _menuView;

    [SerializeField]
    private GameSectionView _audioScreen;

    private StateMachine _gameStateMachine;

    private void Awake()
    {
        _gameStateMachine = new StateMachine();

        _gameStateMachine.Register("AudioScreen", new GameState(_audioScreen));
        _audioScreen.GoToNextScene += NextScene;

        _gameStateMachine.Register("MainMenuState", new MenuState(_menuView));
        _menuView.GoToNextScene += NextScene;

        for (int i = 0; i < _sections.Count ; i++)
        {
            _gameStateMachine.Register($"GameState{i}", new GameState(_sections[i]));
            _sections[i].GoToNextScene += NextScene;
        }

        _gameStateMachine.InitialState = $"AudioScreen";
    }

    private void NextScene(object sender, NextSceneEventArgs e)
    {
        _gameStateMachine.MoveTo(e.NextScene);
    }

    public void ReloadGame()
    {
        SceneManager.LoadScene(0);
    }

}
