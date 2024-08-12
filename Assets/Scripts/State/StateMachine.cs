using System.Collections;
using UnityEngine;

public class StateMachine 
{
    private State _currentState;
    private readonly MonoBehaviour _owner;

    public MonoBehaviour Owner => _owner;

    public StateMachine(MonoBehaviour owner)
    {
        _owner = owner;
    }

    public void ChangeState(State newState)
    {
        _owner.StartCoroutine(ChangeStateCoroutine(newState));
    }
    
    public void ForceChangeState(State newState)
    {
        _currentState.Exit();
        _currentState = newState;
        _currentState.Enter();
    }
    
    private IEnumerator ChangeStateCoroutine(State newState)
    {
        if (_currentState != null)
        {
            yield return _currentState.Exit();
        }

        _currentState = newState;
        _currentState.Enter();
    }
    
    public void Update()
    {
        _currentState?.Update();
    }
}