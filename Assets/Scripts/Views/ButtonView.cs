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
    private bool _selected;

    [SerializeField]
    private List<TextView> _postQuestionText;

    [SerializeField]
    private UnityEvent _buttonAppears;

    [SerializeField]
    private UnityEvent _buttonClicked;

    [SerializeField]
    private UnityEvent _otherButtonClicked;

    [Header("IMAGE ATTRIBUTES")]
    [SerializeField]
    private SpriteRenderer _imgage;

    [SerializeField]
    private List<Sprite> _unSelectedSprites;

    [SerializeField]
    private float _unselectedSpriteSpeed;

    [SerializeField]
    private List<Sprite> _selectedSprites;

    [SerializeField]
    private float _selectedSpriteSpeed;

    private int _nextSpriteIndex;
    private float _timer;
    

    private void Awake()
    {
        _animator = this.GetComponent<Animator>();
        _collider = this.GetComponent<Collider2D>();
        DisableCollision();
    }


    private void Update()
    {
        _timer += Time.deltaTime;
        if (_selected)
        {
            AnimateImage(_selectedSprites,_selectedSpriteSpeed);
        }
        else
        {
            AnimateImage(_unSelectedSprites, _unselectedSpriteSpeed);
        }
    }

    private void AnimateImage(List<Sprite> sprites, float speed)
    {
        if(_nextSpriteIndex >= sprites.Count)
        {
            _nextSpriteIndex = 0;
        }

        if(_timer >= speed)
        {
            _imgage.sprite = sprites[_nextSpriteIndex];
            _timer -= speed;
            _nextSpriteIndex++;
        }
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
        _selected = true;
        _timer = _selectedSpriteSpeed;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        _animator.SetBool("Selected", false);
        _selected = false;
        _timer = _unselectedSpriteSpeed;
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
