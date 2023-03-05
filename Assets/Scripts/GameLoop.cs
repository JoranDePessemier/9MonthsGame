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

    private StateMachine _gameStateMachine;

    private void Awake()
    {
        _gameStateMachine = new StateMachine();

        _gameStateMachine.Register("MainMenuState", new MenuState(_menuView));
        _menuView.GoToNextScene += NextScene;

        for (int i = 0; i < _sections.Count ; i++)
        {
            _gameStateMachine.Register($"GameState{i}", new GameState(_sections[i]));
            _sections[i].GoToNextScene += NextScene;
        }

        _gameStateMachine.InitialState = $"MainMenuState";
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
