using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuView : MonoBehaviour
{
    public event EventHandler<EventArgs> PlayClicked;
    public event EventHandler<NextSceneEventArgs> GoToNextScene;

    public BackGroundView BackGround { get; private set; }

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
        BackGround = this.GetComponent<BackGroundView>();
        _audioManager = FindObjectOfType<AudioManager>();
    }

    public void ButtonClickedPlay()
    {
        OnPlayClicked(EventArgs.Empty);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void GoToSite(string adress)
    {
        Application.OpenURL(adress);
    }

    private void OnPlayClicked(EventArgs eventArgs)
    {
        var handler = PlayClicked;
        handler?.Invoke(this, eventArgs);
    }

    private void OnGoToNextScene(NextSceneEventArgs eventArgs)
    {
        var handler = GoToNextScene;
        handler?.Invoke(this, eventArgs);
    }

    public void NextSection()
    {
        if (_nextSceneName != "")
        {
            OnGoToNextScene(new NextSceneEventArgs(_nextSceneName));
        }

    }

    public void FadeInMusic()
    {
        foreach (string song in _fadeInSongs)
        {
            _audioManager.FadeIn(song, _fadeInSpeed);
        }

        foreach (string song in _fadeOutSongs)
        {
            _audioManager.FadeOut(song, _fadeOutSpeed);
        }
    }
}
