using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NextSceneEventArgs : EventArgs
{
    public NextSceneEventArgs(string nextScene)
    {
        NextScene = nextScene;
    }

    public string NextScene { get; set; }
}


public class GameSectionView : MonoBehaviour
{
    public event EventHandler<NextSceneEventArgs> GoToNextScene;

    [SerializeField]
    private List<TextView> _preQuestionText;

    public List<TextView> PreQuestionText
    {
        get { return _preQuestionText; }
        set { _preQuestionText = value; }
    }
    
    [SerializeField]
    private QuestionTextView _questionText;

    public QuestionTextView QuestionText
    {
        get { return _questionText; }
        set { _questionText = value; }
    }

    [SerializeField]
    private List<ButtonView> _buttons;

    public List<ButtonView> Buttons
    {
        get { return _buttons; }
        set { _buttons = value; }
    }

    [SerializeField]
    private BackGroundView _backGround;

    public BackGroundView BackGround
    {
        get { return _backGround; }
        set { _backGround = value; }
    }

    public List<TextView> PostQuestionText { get; set; }

    public void NextSection()
    {
        OnGoToNextScene(new NextSceneEventArgs(_nextSceneName));
    }

    [SerializeField]
    private string _nextSceneName;

    private void OnGoToNextScene(NextSceneEventArgs eventArgs)
    {
        var handler = GoToNextScene;
        handler?.Invoke(this, eventArgs);
    }
}
