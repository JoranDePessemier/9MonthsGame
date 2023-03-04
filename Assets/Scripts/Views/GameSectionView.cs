using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

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
    public event EventHandler<EventArgs> DoneWaiting;

    public UnityEvent TransferToNextScene;

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

    [SerializeField]
    private float _waitingTime;

    public float WaitingTime
    {
        get { return _waitingTime; }
         private set { _waitingTime = value; }
    }

    [SerializeField]
    private bool _setInactive = true;

    public bool SetInactive
    {
        get { return _setInactive; }
        private set { _setInactive = value; }
    }

    [SerializeField]
    private string _nextSceneName;

    [Header("Music")]
    [SerializeField]
    private string[] _fadeInSongs;

    [SerializeField]
    private float _fadeInSpeed;

    [SerializeField]
    private string[] _fadeOutSongs;

    [SerializeField]
    private float _fadeOutSpeed;

    private AudioManager _audioManager;

    private void Awake()
    {
        _audioManager = FindObjectOfType<AudioManager>();
    }

    public void FadeInMusic()
    {
        foreach(string song in _fadeInSongs)
        {
            _audioManager.FadeIn(song, _fadeInSpeed);
        }

        foreach(string song in _fadeOutSongs)
        {
            _audioManager.FadeOut(song, _fadeOutSpeed);
        }
    }

    public void NextSection()
    {
        if(_nextSceneName != "")
        {
            OnGoToNextScene(new NextSceneEventArgs(_nextSceneName));
        }

    }

    private void OnGoToNextScene(NextSceneEventArgs eventArgs)
    {
        var handler = GoToNextScene;
        handler?.Invoke(this, eventArgs);
    }

    public void Wait() 
    {
        StartCoroutine(ProcessWaiting());
    }

    private IEnumerator ProcessWaiting()
    {
        yield return new WaitForSeconds(_waitingTime);
        _waitingTime = 0;
        OnDoneWaiting(EventArgs.Empty);
    }

    private void OnDoneWaiting(EventArgs eventArgs)
    {
        var handler = DoneWaiting;
        handler?.Invoke(this, eventArgs);
    }
}
