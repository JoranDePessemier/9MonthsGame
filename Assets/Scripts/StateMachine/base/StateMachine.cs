using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateMachine
{
    private Dictionary<string, GameState> _states = new Dictionary<string, GameState>();

    private Stack<string> _currentStateNames;

    public GameState CurrentState => _states[_currentStateNames.Peek()];

    public string InitialState
    {
        set
        {
            _currentStateNames.Push(value);
            CurrentState.OnEnter();
        }
    }

    public void Register(string stateName, GameState state)
    {
        state.StateMachine = this;
        _states.Add(stateName, state);
    }

    public void MoveTo(string stateName)
    {
        CurrentState.OnExit();

        _currentStateNames.Push(stateName);

        CurrentState.OnEnter();
        CurrentState.OnResume();
    }

    public void Push(string stateName)
    {
        CurrentState.OnSuspend();

        _currentStateNames.Push(stateName);

        CurrentState.OnEnter();
        CurrentState.OnResume();
    }

    public void Pop()
    {
        CurrentState.OnSuspend();
        CurrentState.OnExit();

        _currentStateNames.Pop();

        CurrentState.OnResume();
    }
}
