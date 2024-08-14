using System;
using UnityEngine;

[RequireComponent(typeof(Weapon))]
public class Player : Character
{
    [SerializeField] private PlayerStats baseStats;
    private float _maxHealth;
    
    public event Action<StatType, float> StatChange;
    public event Action<float> HealthChange;

    private void Awake()
    {
        currentStats = baseStats.Stats;
        _maxHealth = baseStats.Stats.Health;
        // GetComponent<Health>().IsDead += TargetDead;
    }
    
    private void TargetDead()
    {
        StateMachine.ForceChangeState(new IdleState(StateMachine));
    }

    protected override void Start()
    {
        base.Start();
        
        HealthChange?.Invoke(_maxHealth);
        StateMachine.SetCommonParameters(baseStats.Stats.TimeToPrepareAttack, Weapon.GetWeaponStats(), Target);
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
                currentStats.Health += value;
                currentStats.Health = Mathf.Clamp(currentStats.Health, 1, _maxHealth);
                HealthChange?.Invoke(currentStats.Health);
                break;
            case StatType.Restored:
                GetComponent<Health>().Heal(value);
                break;
            case StatType.Armor:
                currentStats.Armor += value;
                break;
            case StatType.AttackPower:
                currentStats.AttackPower += value;
                break;
            case StatType.TimeToPrepareAttack:
                currentStats.TimeToPrepareAttack += value;
                break;
            case StatType.Luck:
                currentStats.Luck += value;
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
