using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class TextView : MonoBehaviour
{
    public event EventHandler<EventArgs> TextDone;

    TextMeshPro _textObject;
    MouseIconView _mouseIcon;
    AudioManager _audioManager;

    [SerializeField]
    private float _scrollSpeed;


    [SerializeField]
    private UnityEvent _characterAppears;

    [SerializeField]
    private UnityEvent _textIsDoneScrolling;

    [SerializeField]
    private UnityEvent _textClicked;

    

    private void Awake()
    {
        _textObject = this.GetComponent<TextMeshPro>();
        _textObject.maxVisibleCharacters = 0;
    }

    private void Start()
    {
        _mouseIcon = FindObjectOfType<MouseIconView>();
        _audioManager = FindObjectOfType<AudioManager>();
    }

    public void BeginText()
    {
        if (_audioManager == null)
        {
            _audioManager = FindObjectOfType<AudioManager>();
        }
        StartCoroutine(ScrollText());


    }

    private IEnumerator ScrollText()
    {
        for (int i = 0; i < _textObject.text.Length; i++)
        {
            _textObject.maxVisibleCharacters = i + 1;
            _characterAppears.Invoke();

            if(i+1 < _textObject.text.Length &&_textObject.text[i+1] != ' ')
            {
                string[] sounds = new string[]{"CharacterAppears", "CharacterAppears2", "CharacterAppears3"};
                _audioManager.Play(sounds, 0.1f);
            }


            yield return new WaitForSeconds(_scrollSpeed);
        }
        
        ScrollingCompleted();

        while (!Input.GetMouseButtonDown(0))
        {
            yield return 0;
        }

        TextClicked();
        
    }

    private void TextClicked()
    {
        _mouseIcon?.DeActivate();
        _audioManager.Play("TextClicked",0.1f);
        _textClicked.Invoke();
        OnTextDone(EventArgs.Empty);
    }

    private  void  ScrollingCompleted()
    {
        _mouseIcon?.Activate();
        _textIsDoneScrolling.Invoke();
    }

    private void DisableText()
    {
        this.transform.parent.gameObject.SetActive(false);
    }

    private void OnTextDone(EventArgs eventArgs)
    {
        var handler = TextDone;
        handler?.Invoke(this, eventArgs);
    }
}
