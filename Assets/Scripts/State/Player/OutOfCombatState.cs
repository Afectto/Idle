using System.Collections;
using UnityEngine;

public class OutOfCombatState : State
{
    private StateMachine _stateMachine;
    public OutOfCombatState(StateMachine stateMachine)
    {
        _stateMachine = stateMachine;
    }
    public override void Enter()
    {
        Debug.Log($"{_stateMachine.Owner.GetType().Name} Enter Out Of Combat");
    }

    public override IEnumerator Exit(bool isForce = false, float duration = 0)
    {        
        Debug.Log($"{_stateMachine.Owner.GetType().Name} Exit Out Of Combat");
        yield return base.Exit(isForce, duration);
    }

    public override void Update()
    {
    }
}
