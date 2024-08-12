using System;
using UnityEngine;

[RequireComponent(typeof(Weapon))]
public class Player : Character
{
    [SerializeField] private PlayerStats baseStats;
    private Weapon _weapon;
    private Stats _currentStats;
    private float _maxHealth;

    public Action<StatType, float> StatChange;
    public Action<float, float> HealthChange;

    protected override void Start()
    {
        base.Start();
        _weapon = GetComponent<Weapon>();
        _currentStats = baseStats.Stats;
        _maxHealth = baseStats.Stats.Health;
        HealthChange?.Invoke(_currentStats.Health, _maxHealth);
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
                _currentStats.Health += value;
                _currentStats.Health = Mathf.Clamp(_currentStats.Health, 0, _maxHealth);
                HealthChange?.Invoke(_currentStats.Health, _maxHealth);
                break;
            case StatType.Armor:
                _currentStats.Armor += value;
                break;
            case StatType.AttackPower:
                _currentStats.AttackPower += value;
                break;
            case StatType.TimeToPrepareAttack:
                _currentStats.TimeToPrepareAttack += value;
                break;
            case StatType.Luck:
                _currentStats.Luck += value;
                break;
        }
        
        StatChange?.Invoke(statType, value);
    }

    public void AddMaxHealth(float value)
    {
        _maxHealth += value;
    }

    private void StartFight()
    {
        StateMachine.ChangeState(new PrepareToFightState(StateMachine, _currentStats.TimeToPrepareAttack, _weapon.GetWeaponStats()));
    }


    
}
