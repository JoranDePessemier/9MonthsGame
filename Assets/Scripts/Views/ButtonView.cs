using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class ButtonClickedEventArgs : EventArgs
{
    public ButtonClickedEventArgs(ButtonView clickedButton ,List<TextView> postQuestionText)
    {
        ClickedButton = clickedButton;
        PostQuestionText = postQuestionText;
    }

    public List<TextView>  PostQuestionText { get; set; }

    public ButtonView ClickedButton { get; set; }
}

public class ButtonView : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
{
    public event EventHandler<ButtonClickedEventArgs> ButtonClicked;

    private Animator _animator;
    private Collider2D _collider;

    [SerializeField]
    private List<TextView> _postQuestionText;

    [SerializeField]
    private UnityEvent _buttonAppears;

    [SerializeField]
    private UnityEvent _buttonClicked;

    [SerializeField]
    private UnityEvent _otherButtonClicked;

    private void Awake()
    {
        _animator = this.GetComponent<Animator>();
        _collider = this.GetComponent<Collider2D>();
        DisableCollision();
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        OnButtonCLicked(new ButtonClickedEventArgs(this,_postQuestionText));
        DisableCollision();
        _buttonClicked.Invoke();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        _animator.SetBool("Selected", true);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        _animator.SetBool("Selected", false);
    }
     public void ShowButton()
    {
        EnableCollision();
        _buttonAppears.Invoke();
    }

    private void EnableCollision()
    {
        _collider.enabled = true;
    }

    internal void NotClicked()
    {
        DisableCollision();
        _otherButtonClicked.Invoke();
    }

    private void DisableCollision()
    {
        _collider.enabled = false;
    }

    private void OnButtonCLicked(ButtonClickedEventArgs eventArgs)
    {
        var handler = ButtonClicked;
        handler?.Invoke(this, eventArgs);
    }
}
