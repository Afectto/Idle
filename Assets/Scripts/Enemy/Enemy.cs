using UnityEngine;

public class Enemy : Character
{
    [SerializeField] private SpriteRenderer skin;
    private EnemyStats _stats;

    public EnemyStats CurrentStats => _stats;

    protected override void Awake()
    {
        base.Awake();
        gameObject.SetActive(false);
    }

    protected override void OnEnterAttackState()
    {
        
    }

    protected override void OnEnterPrepareToFightState()
    {
        Target = FindObjectOfType<Player>();
        if (Target)
        {
            Target.GetComponent<Health>().IsDead += TargetDead;
            StateMachine.SetCommonParameters(_stats.Stats.TimeToPrepareAttack, GetComponent<Weapon>().GetWeaponStats(),
                Target);
            gameObject.SetActive(true);
        }
    }

    protected override void OnEnterIdleState()
    {
    }

    protected override void OnEnterOutOfCombatState()
    {
        gameObject.SetActive(false);
    }

    protected override void OnEnterChangeWeaponState()
    {
    }

    private void TargetDead()
    {
        StateMachine.ForceChangeState(new OutOfCombatState(StateMachine));
    }

    public void SetNewStats(EnemyStats newStats)
    {
        if(StateMachine == null) return;
        
        _stats = newStats;
        skin.sprite = _stats.Skin;
        GetComponent<Health>().Initialize(_stats.Stats.Health);
        StateMachine.SetCommonParameters(_stats.Stats.TimeToPrepareAttack, GetComponent<Weapon>().GetWeaponStats(), Target);
        StateMachine.ForceChangeState(new PrepareToFightState(StateMachine));
    }

    public override Stats GetCurrentStats()
    {
        return _stats.Stats;
    }

    private void OnDestroy()
    {
        if (StateMachine != null)
        {
            StateMachine.OnChangeState -= ChangeState;
        }
        if (Target)
        {
            Target.GetComponent<Health>().IsDead -= TargetDead;
        }
        GetComponent<Health>().IsDead -= TargetDead;
    }
}
