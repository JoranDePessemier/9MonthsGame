using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackGroundView : MonoBehaviour
{
    private SpriteRenderer _renderer;

    public event EventHandler<EventArgs> FadeComplete;

    [SerializeField]
    private float _fadeSpeed;

    private void Awake()
    {
        _renderer = this.gameObject.GetComponent<SpriteRenderer>();
        this.gameObject.SetActive(true);
    }

    public void FadeOut()
    {
        StartCoroutine(FadeUpdate());
    }

    private IEnumerator FadeUpdate()
    {
        while(_renderer.color.a > 0)
        {
            _renderer.color = new Color(_renderer.color.r,_renderer.color.g,_renderer.color.b, Mathf.MoveTowards(_renderer.color.a, 0, _fadeSpeed * Time.deltaTime));
            yield return 0;
        }

        OnFadeComplete(EventArgs.Empty);

        this.gameObject.SetActive(false);
    }

    private void OnFadeComplete(EventArgs eventArgs)
    {
        var handler = FadeComplete;
        handler?.Invoke(this, eventArgs);
    }
}
