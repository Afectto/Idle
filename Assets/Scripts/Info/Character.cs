using System;
using UnityEngine;

public abstract class Character : MonoBehaviour
{
    protected StateMachine StateMachine;
    [SerializeField]protected Stats currentStats;
    protected Weapon Weapon;
    protected Character Target;

    public State GetCurrentState() => StateMachine?.GetCurrentState();
    public virtual Stats GetCurrentStats() => currentStats;
    public event Action<State> OnChangeState; 
    
    protected virtual void Start()
    {
        StateMachine = new StateMachine(this);
        StateMachine.OnChangeState += ChangeState;
        Weapon = GetComponent<Weapon>();
        OutOfFight();
    }

    public void ChangeWeapon(WeaponStats stats)
    {
        var currentStateName = StateMachine.GetCurrentState().GetType().Name;
        
        if (currentStateName == nameof(AttackState))
        {
            void StateChangeHandler(object state) 
            {
                StateMachine.ChangeState(new ChangeWeaponState(StateMachine, Weapon, stats));
                StateMachine.OnChangeState -= StateChangeHandler; 
            }

            StateMachine.OnChangeState += StateChangeHandler;
        }
        else if(currentStateName == nameof(OutOfCombatState))
        {
            Weapon.ChangeWeapon(stats);
        }
        else
        {
            StateMachine.ForceChangeState(new ChangeWeaponState(StateMachine, Weapon, stats));
        }
    }

    public WeaponStats GetWeaponStats()
    {
        return Weapon.GetWeaponStats();
    }

    protected virtual void Update()
    {
        StateMachine.Update();
    }
    
    public void OutOfFight()
    {
        StateMachine.ChangeState(new OutOfCombatState(StateMachine));
    }
    
    public void StartFight()
    {
        StateMachine.ChangeState(new PrepareToFightState(StateMachine));
    }

    protected void ChangeState(State state)
    {
        OnChangeState?.Invoke(state);
        switch (state.GetType().Name)
        {
            case nameof(AttackState):
                OnEnterAttackState();
                break;
            case nameof(PrepareToFightState):
                OnEnterPrepareToFightState();
                break;
            case nameof(IdleState):
                OnEnterIdleState();
                break;
            case nameof(OutOfCombatState):
                OnEnterOutOfCombatState();
                break;
            case nameof(ChangeWeaponState):
                OnEnterChangeWeaponState();
                break;
        }
    }

    protected abstract void OnEnterAttackState();
    protected abstract void OnEnterPrepareToFightState();
    protected abstract void OnEnterIdleState();
    protected abstract void OnEnterOutOfCombatState();
    protected abstract void OnEnterChangeWeaponState();




}