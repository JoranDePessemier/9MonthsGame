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
        _gameSection.PreQuestionText[0].BeginText();
    }
}
