using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
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

    private void Awake()
    {
        _animator = this.GetComponent<Animator>();
        this.GetComponent<SpriteRenderer>().enabled = false;
        _collider = this.GetComponent<Collider2D>();
        DisableCollision();
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        OnButtonCLicked(new ButtonClickedEventArgs(this,_postQuestionText));
        this.gameObject.SetActive(false);
        DisableCollision();
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
        this.GetComponent<SpriteRenderer>().enabled = true;
        EnableCollision();
    }

    private void EnableCollision()
    {
        _collider.enabled = true;
    }

    internal void NotClicked()
    {
        DisableCollision();
        this.GetComponent<SpriteRenderer>().enabled = false;
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
