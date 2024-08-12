using System.Collections;
using UnityEngine;

public class PrepareToFightState : State
{
    private float _preparationTime;
    private float _timeLeft;
    private StateMachine _stateMachine;
    private bool _isChangeStateToAttack;
    private WeaponStats _weaponStats;

    public PrepareToFightState(StateMachine stateMachine, float preparationTime, WeaponStats currentWeaponStats)
    {
        _stateMachine = stateMachine;
        _preparationTime = preparationTime;
        _weaponStats = currentWeaponStats;
    }

    public override void Enter()
    {
        Debug.Log("Character is Prepare To Fight.");
        _timeLeft = _preparationTime;
        _isChangeStateToAttack = false;
    }

    public override IEnumerator Exit()
    {
        yield return null;
    }

    public override void Update()
    {
        if (_timeLeft > 0)
        {
            _timeLeft -= Time.deltaTime;
        }
        else if(!_isChangeStateToAttack)
        {
            _isChangeStateToAttack = true;
            _stateMachine.ChangeState(new AttackState(_stateMachine, _preparationTime, _weaponStats)); 
        }
    }
}