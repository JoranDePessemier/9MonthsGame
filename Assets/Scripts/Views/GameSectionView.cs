using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSectionView : MonoBehaviour
{
    [SerializeField]
    private List<TextView> _preQuestionText;

    public List<TextView> PreQuestionText
    {
        get { return _preQuestionText; }
        set { _preQuestionText = value; }
    }

    private TextView _questionText;

    private List<ButtonView> _buttons;
}
