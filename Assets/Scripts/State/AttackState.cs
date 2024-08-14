using System.Collections;
using UnityEngine;
public class AttackState : State
{
    private StateMachine _stateMachine;
    private Character _character;
    private float _attackDuration;

    public AttackState(StateMachine stateMachine)
    {
        _stateMachine = stateMachine;
        _character = stateMachine.Owner as Character;
        _attackDuration = stateMachine.GetCurrentWeaponStats().AttackDuration; 
    }

    public override void Enter()
    {
        Debug.Log("Character is attacking.");
        _character.StartCoroutine(PerformAttack());
    }

    public override IEnumerator Exit(bool isForce = false, float duration = 0)
    {
        yield return base.Exit(isForce, duration);
    }

    public override void Update()
    {
    }

    private IEnumerator PerformAttack()
    {
        yield return _character.StartCoroutine(Exit(false, _attackDuration));
        
        var weaponStats = _stateMachine.GetCurrentWeaponStats();
        var healthTarget = _stateMachine.GetCurrentTarget().GetComponent<Health>();

        int damage = CalculateDamage(weaponStats);

        healthTarget.SetDamage(damage);
        _stateMachine.ChangeState(new PrepareToFightState(_stateMachine));
    }

    private int CalculateDamage(WeaponStats weaponStats)
    {
        float baseDamage = weaponStats.Damage;
        float attackPower = _character.GetCurrentStats().AttackPower;
        float enemyArmor = _stateMachine.GetCurrentTarget().GetCurrentStats().Armor;

        int totalDamage = Mathf.Max(0, (int)(baseDamage + attackPower - enemyArmor));
        Debug.Log($"Total Damage: {totalDamage}");
        return totalDamage;
    }
}
