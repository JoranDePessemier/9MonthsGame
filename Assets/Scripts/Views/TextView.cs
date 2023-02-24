using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class TextView : MonoBehaviour
{
    TextMeshPro _textObject;
    MouseIconView _mouseIcon;

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
            _characterAppears.Invoke();
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
        _textClicked.Invoke();
        
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
}
