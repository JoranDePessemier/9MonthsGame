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
    }
}
