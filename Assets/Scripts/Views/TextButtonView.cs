using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class TextButtonView : MonoBehaviour, IPointerClickHandler
{
    private AudioManager _audioManager;
    
    [SerializeField]
    private UnityEvent _buttonClicked;

    private void Start()
    {
        _audioManager = FindObjectOfType<AudioManager>();
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        _buttonClicked.Invoke();
        _audioManager.Play("TextClicked");
    }
}


