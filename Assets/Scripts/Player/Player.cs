using System;
using UnityEngine;

[RequireComponent(typeof(Weapon))]
public class Player : Character
{
    [SerializeField] private PlayerStats baseStats;
    [SerializeField]private Enemy _target;
    // private Stats _currentStats;
    private float _maxHealth;


    public event Action<StatType, float> StatChange;
    public event Action<float> HealthChange;

    private void Awake()
    {
        currentStats = baseStats.Stats;
        _maxHealth = baseStats.Stats.Health;
        _target.GetComponent<Health>().IsDead += TargetDead;
        GetComponent<Health>().IsDead += TargetDead;
    }
    
    private void TargetDead()
    {
        StateMachine.ForceChangeState(new IdleState());
    }

    protected override void Start()
    {
        base.Start();
        
        StateMachine.ChangeState(new OutOfCombatState());
        HealthChange?.Invoke(_maxHealth);
        StateMachine.SetCommonParameters(baseStats.Stats.TimeToPrepareAttack, weapon.GetWeaponStats(), _target);
    }

    protected override void Update()
    {
        base.Update();
        if (Input.GetKeyDown(KeyCode.Space))
        {
            StartFight();
        }

        if (Input.GetKeyDown(KeyCode.A))
        {
            StartIdle();
        }
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
    
    private void StartFight()
    {
        if (_target)
        {
            StateMachine.ChangeState(new PrepareToFightState(StateMachine));
        }
    }
}
