using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class MouseIconView : MonoBehaviour
{
    [SerializeField]
    private UnityEvent _activate;

    [SerializeField]
    private UnityEvent _deActivate;

    public void Activate()
    {
        _activate.Invoke();
    }

    public void DeActivate()
    {
        _deActivate.Invoke();
    }
}
