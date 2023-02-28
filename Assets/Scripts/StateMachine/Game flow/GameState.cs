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
        base.OnEnter();
        _gameSection.gameObject.SetActive(true);
        StartPreText();
    }

    public override void OnExit()
    {
        base.OnExit();
        _gameSection.gameObject.SetActive(false);
    }

    private void StartPreText()
    {
        if(_gameSection.PreQuestionText.Count > 0)
        {
            _gameSection.PreQuestionText[0].BeginText();
            _gameSection.PreQuestionText[0].TextDone += NextPreText;
        }
        else
        {
            StartQuestionText();
        }

    }

    private void NextPreText(object sender, EventArgs e)
    {
        _gameSection.PreQuestionText[0].TextDone -= NextPreText;
        _gameSection.PreQuestionText.RemoveAt(0);

        StartPreText();
    }

    private void StartQuestionText()
    {
        if (_gameSection.QuestionText != null)
        {
            _gameSection.QuestionText.BeginText();
            _gameSection.QuestionText.TextDone += ShowButtons;
        }
        else
        {
            FadeBackGround();
        }
    }

    private void FadeBackGround()
    {
        FadeMusic();

        if(_gameSection.BackGround != null)
        {
            _gameSection.BackGround.FadeOut();
            _gameSection.BackGround.FadeComplete += NextScene;
        }
        else
        {
            NextScene(this, EventArgs.Empty);
        }
    }

    private void FadeMusic()
    {
        _gameSection.FadeInMusic();
    }

    private void NextScene(object sender, EventArgs e)
    {
        _gameSection.NextSection();
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
        else
        {
            FadeBackGround();
        }
    }

}
