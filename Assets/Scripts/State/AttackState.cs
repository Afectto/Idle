using System.Collections;
using UnityEngine;

public class AttackState : State
{
    private StateMachine _stateMachine;
    private Character _character;
    private float _preparationTime;
    private float _attackDuration;
    private WeaponStats _currentWeaponStats;

    public AttackState(StateMachine stateMachine, float preparationTime, WeaponStats currentWeaponStats)
    {
        _stateMachine = stateMachine;
        _preparationTime = preparationTime;
        _character = stateMachine.Owner as Character;
        _attackDuration = currentWeaponStats.AttackDuration;
        _currentWeaponStats = currentWeaponStats;
    }

    public override void Enter()
    {
        Debug.Log("Character is attacking.");
        _character.StartCoroutine( PerformAttack());
    }

    public override IEnumerator Exit()
    {
        yield return new WaitForSeconds(_attackDuration);
    }

    public override void Update()
    {
    }

    private IEnumerator PerformAttack()
    {
        yield return _character.StartCoroutine(Exit());
        _stateMachine.ChangeState(new PrepareToFightState(_stateMachine, _preparationTime, _currentWeaponStats));
    }
}