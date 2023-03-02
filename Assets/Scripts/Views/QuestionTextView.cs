using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class QuestionTextView : MonoBehaviour
{


    public event EventHandler<EventArgs> TextDone;

    private TextMeshPro _textObject;
    private Transform _transform;
    MouseIconView _mouseIcon;
    private AudioManager _audioManager;

    [SerializeField]
    private float _scrollSpeed;

    [SerializeField]
    private Vector3 _secondPosition;

    [SerializeField]
    private float _secondPositionMoveSpeed = 1;

    [SerializeField]
    private UnityEvent _characterAppears;

    [SerializeField]
    private UnityEvent _textIsDoneScrolling;

    [SerializeField]
    private UnityEvent _buttonClicked;


    private void Awake()
    {
        _textObject = this.GetComponent<TextMeshPro>();
        _textObject.maxVisibleCharacters = 0;

        _transform = this.transform.parent.transform;

    }

    private void Start()
    {
        _audioManager = FindObjectOfType<AudioManager>();
        _mouseIcon = FindObjectOfType<MouseIconView>();
    }


    public void BeginText()
    {
        StartCoroutine(ScrollText());
    }

    private IEnumerator ScrollText()
    {
        for (int i = 0; i < _textObject.text.Length; i++)
        {
            _textObject.maxVisibleCharacters = i + 1;

            if (i + 1 < _textObject.text.Length && _textObject.text[i + 1] != ' ')
            {
                _audioManager.Play("CharacterAppears", 0.1f);
            }

            _characterAppears.Invoke();
            yield return new WaitForSeconds(_scrollSpeed);
        }

        ScrollingCompleted();
    }

    private void OnDrawGizmosSelected()
    {
        Debug.DrawLine(transform.position, _secondPosition);
    }

    private void ScrollingCompleted()
    {
        _textIsDoneScrolling.Invoke();
        OnTextDone(EventArgs.Empty);
        _mouseIcon?.Activate();

        StartCoroutine(MoveToSecondPosition());
    }

    private IEnumerator MoveToSecondPosition()
    {
        while (Vector3.Distance(_transform.position,_secondPosition) > 0.01f)
        {
            _transform.position = Vector3.Lerp(_transform.position, _secondPosition, _secondPositionMoveSpeed*Time.deltaTime);
            yield return 0;
        }
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

    public void ButtonClicked()
    {
        _mouseIcon?.DeActivate();
        _buttonClicked.Invoke();
    }

}
