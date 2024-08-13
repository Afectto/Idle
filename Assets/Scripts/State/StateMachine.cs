using System;
using System.Collections;

public class StateMachine 
{
    private State _currentState;
    private readonly Character _owner;

    public Character Owner => _owner;
    private float _preparationTime;
    private WeaponStats _currentWeaponStats;  
    private Character _target;  
    
    public float PreparationTime => _preparationTime;
    public WeaponStats GetCurrentWeaponStats() => _currentWeaponStats;
    public Character GetCurrentTarget() => _target;

    public event Action<State> OnChangeState;
    
    public StateMachine(Character owner)
    {
        _owner = owner;
    }
    
    public void SetCommonParameters(float preparationTime, WeaponStats currentWeaponStats, Character target)
    {
        _preparationTime = preparationTime;
        _currentWeaponStats = currentWeaponStats;
        _target = target;
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
        
        OnChangeState?.Invoke(_currentState);
    }
    
    public void Update()
    {
        _currentState?.Update();
    }
}