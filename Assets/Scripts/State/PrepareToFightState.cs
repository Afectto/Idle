using System.Collections;
using UnityEngine;

public class PrepareToFightState : State
{    
    private float _timeLeft;
    private StateMachine _stateMachine;
    private bool _isChangeStateToAttack;

    public PrepareToFightState(StateMachine stateMachine)
    {
        _stateMachine = stateMachine;
        Debug.Log($"{_stateMachine.Owner.GetType().Name} is INITIAL Prepare To Fight.");
        
    }

    public override void Enter()
    {
        Debug.Log($"{_stateMachine.Owner.GetType().Name} is ENTER Prepare To Fight.");
        _timeLeft = _stateMachine.PreparationTime;
        _isChangeStateToAttack = false;
    }

    public override IEnumerator Exit(bool isForce = false, float duration = 0)
    {
       yield return base.Exit(isForce, duration);
       Debug.Log($"{_stateMachine.Owner.GetType().Name} is EXIT Prepare To Fight.");
    }

    public override void Update()
    {
        if (_timeLeft > 0)
        {
            _timeLeft -= Time.deltaTime;
        }
        else if (!_isChangeStateToAttack)
        {
            _isChangeStateToAttack = true;
            Debug.Log($"{_stateMachine.Owner.GetType().Name} is Change Prepare To Fight.");
            
            _stateMachine.ChangeState(new AttackState(_stateMachine)); 
        }
    }
}