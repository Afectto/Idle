﻿using System.Collections;

public class IdleState : State
{
    private StateMachine _stateMachine;

    public IdleState(StateMachine stateMachine)
    {
    _stateMachine = stateMachine;
    }
    public override void Enter()
    {
    }
    
    public override IEnumerator Exit(bool isForce = false, float duration = 0)
    {
        yield return base.Exit(isForce, duration);
    }
    
    public override void Update() { /* Логика для состояния ожидания */ }
}