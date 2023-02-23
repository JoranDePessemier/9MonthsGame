using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class ButtonClickedEventArgs : EventArgs
{
    //public TextMeshProUGUI  { get; set; }
}

public class ButtonView : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
{
    public event EventHandler<EventArgs> ButtonClicked;

    private Animator _animator;

    private void Awake()
    {
        _animator = this.GetComponent<Animator>();
    }

    public void OnPointerClick(PointerEventData eventData)
    {

    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        _animator.SetBool("Selected", true);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        _animator.SetBool("Selected", false);
    }
}
