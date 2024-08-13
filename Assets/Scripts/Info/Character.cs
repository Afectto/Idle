using System;
using UnityEngine;

public abstract class Character : MonoBehaviour
{
    protected StateMachine StateMachine;
    protected Stats currentStats;
    protected Weapon weapon;

    public virtual Stats GetCurrentStats() => currentStats;
    public event Action<State> OnChangeState; 
    
    protected virtual void Start()
    {
        StateMachine = new StateMachine(this);
        StateMachine.OnChangeState += HandleChangeState;
        weapon = GetComponent<Weapon>();
        StartIdle();
    }

    public WeaponStats GetWeaponStats()
    {
        return weapon.GetWeaponStats();
    }

    protected void HandleChangeState(State currentState)
    {
        OnChangeState?.Invoke(currentState);
    }

    protected virtual void Update()
    {
        StateMachine.Update();
    }
    
    protected void StartIdle()
    {
        StateMachine.ChangeState(new IdleState());
    }

}