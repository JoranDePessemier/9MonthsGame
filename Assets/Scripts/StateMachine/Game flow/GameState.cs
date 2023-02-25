using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameState : State
{
    private GameSectionView _gameSection;

    public GameState(GameSectionView gameSection)
    {
        _gameSection = gameSection;
    }

    public override void OnEnter()
    {
        _gameSection.gameObject.SetActive(true);
        StartPreText();
    }

    private void StartPreText()
    {
        _gameSection.PreQuestionText[0].BeginText();
        _gameSection.PreQuestionText[0].TextDone += NextPreText;
    }

    private void NextPreText(object sender, EventArgs e)
    {
        _gameSection.PreQuestionText[0].TextDone -= NextPreText;
        _gameSection.PreQuestionText.RemoveAt(0);

        if(_gameSection.PreQuestionText.Count > 0)
        {
            StartPreText();
        }
        else if(_gameSection.QuestionText != null)
        {
            StartQuestionText();
        }
    }

    private void StartQuestionText()
    {
        _gameSection.QuestionText.BeginText();
        _gameSection.QuestionText.TextDone += ShowButtons;
    }

    private void ShowButtons(object sender, EventArgs e)
    {
        _gameSection.QuestionText.TextDone -= ShowButtons;

        foreach(ButtonView button in _gameSection.Buttons)
        {
            button.ShowButton();
            button.ButtonClicked += ButtonClicked;
        }

        
    }

    private void ButtonClicked(object sender, ButtonClickedEventArgs e)
    {
        foreach (ButtonView button in _gameSection.Buttons)
        {
            button.ButtonClicked -= ButtonClicked;
            
            if(e.ClickedButton != button)
            {
                button.NotClicked();
            }
        }

        _gameSection.QuestionText.ButtonClicked();

        if (e.PostQuestionText.Count > 0)
        {
            _gameSection.PostQuestionText = e.PostQuestionText;
            StartPostText();
        }
    }

    private void StartPostText()
    {
        _gameSection.PostQuestionText[0].BeginText();
        _gameSection.PostQuestionText[0].TextDone += NextPostText;
    }

    private void NextPostText(object sender, EventArgs e)
    {
        _gameSection.PostQuestionText[0].TextDone -= NextPreText;
        _gameSection.PostQuestionText.RemoveAt(0);

        if (_gameSection.PostQuestionText.Count > 0)
        {
            StartPostText();
        }
    }
}
