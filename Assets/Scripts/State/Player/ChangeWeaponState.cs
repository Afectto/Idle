using System.Collections;

public class ChangeWeaponState : State
{
    private StateMachine _stateMachine;
    private Weapon _weapon;
    private WeaponStats _weaponStats;
    private const float ChangeDuration = 2;

    public float GetChangeDuration() => ChangeDuration;
    
    public ChangeWeaponState(StateMachine stateMachine, Weapon weapon, WeaponStats newStats)
    {
        _stateMachine = stateMachine;
        _weapon = weapon;
        _weaponStats = newStats;
    }

    public override void Enter()
    {
        _stateMachine.Owner.StartCoroutine(PerformChangeWeapon());
    }

    public override IEnumerator Exit(bool isForce = false, float duration = 0)
    {
        yield return base.Exit(isForce, duration);
    }
    public override void Update() { /* Логика для смены оружия */ }

    private IEnumerator PerformChangeWeapon()
    {
        yield return _stateMachine.Owner.StartCoroutine(Exit(false, ChangeDuration));
        
        _weapon.ChangeWeapon(_weaponStats);
        _stateMachine.ChangeState(new PrepareToFightState(_stateMachine));
    }
}