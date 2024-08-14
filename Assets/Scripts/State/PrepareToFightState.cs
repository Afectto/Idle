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
        
    }

    public override void Enter()
    {
        _timeLeft = _stateMachine.PreparationTime;
        _isChangeStateToAttack = false;
    }

    public override IEnumerator Exit(bool isForce = false, float duration = 0)
    {
       yield return base.Exit(isForce, duration);
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
            
            _stateMachine.ChangeState(new AttackState(_stateMachine)); 
        }
    }
}