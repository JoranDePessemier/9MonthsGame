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
    
    [SerializeField]
    private QuestionTextView _questionText;

    public QuestionTextView QuestionText
    {
        get { return _questionText; }
        set { _questionText = value; }
    }

    [SerializeField]
    private List<ButtonView> _buttons;

    public List<ButtonView> Buttons
    {
        get { return _buttons; }
        set { _buttons = value; }
    }

    [SerializeField]
    private BackGroundView _backGround;

    public BackGroundView BackGround
    {
        get { return _backGround; }
        set { _backGround = value; }
    }

    public List<TextView> PostQuestionText { get; set; } 



}
