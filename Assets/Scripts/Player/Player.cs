using System;
using UnityEngine;

[RequireComponent(typeof(Weapon))]
public class Player : Character
{
    [SerializeField] private PlayerStats baseStats;
    public event Action<StatType, float> StatChange;
    public event Action<float> HealthChange;
    public event Action IsDead;
    
    protected override void Awake()
    {
        base.Awake();
        CurrentStats = baseStats.Stats;
        GetComponent<Health>().IsDead += PlayerDead;
        StateMachine.SetCommonParameters(baseStats.Stats.TimeToPrepareAttack, Weapon.GetWeaponStats(), Target);
    }

    private void PlayerDead()
    {
        IsDead?.Invoke();
        StateMachine.ForceChangeState(new OutOfCombatState(StateMachine));
        gameObject.SetActive(true);
        GetComponent<Health>().Heal(CurrentStats.Health);
    }

    private void TargetDead()
    {
        StateMachine.ForceChangeState(new IdleState(StateMachine));
    }
    
    protected override void OnEnterAttackState()
    {
    }

    protected override void OnEnterPrepareToFightState()
    {
        Target = FindObjectOfType<Enemy>();
        if (Target)
        {
            Target.GetComponent<Health>().IsDead += TargetDead;
            StateMachine.SetCommonParameters(baseStats.Stats.TimeToPrepareAttack, Weapon.GetWeaponStats(), Target);
        }
    }

    protected override void OnEnterIdleState()
    {
    }

    protected override void OnEnterOutOfCombatState()
    {
        if (Target != null)
        {
            Target.GetComponent<Health>().IsDead -= TargetDead;
        }
    }

    protected override void OnEnterChangeWeaponState()
    {
        Target = FindObjectOfType<Enemy>();
        Target.GetComponent<Health>().IsDead += TargetDead;
    }

    public void ChangeStat(StatType statType, float value)
    {
        switch (statType)
        {
            case StatType.Health:
                CurrentStats.Health += value;
                HealthChange?.Invoke(CurrentStats.Health);
                break;
            case StatType.Restored:
                GetComponent<Health>().Heal(value);
                break;
            case StatType.Armor:
                CurrentStats.Armor += value;
                break;
            case StatType.AttackPower:
                CurrentStats.AttackPower += value;
                break;
            case StatType.TimeToPrepareAttack:
                CurrentStats.TimeToPrepareAttack += value;
                break;
            case StatType.Luck:
                CurrentStats.Luck += value;
                break;
        }

        StatChange?.Invoke(statType, value);
    }

    private void OnDestroy()
    {
        if (Target)
        {
            Target.GetComponent<Health>().IsDead -= TargetDead;
        }

        GetComponent<Health>().IsDead -= TargetDead;
    }
}