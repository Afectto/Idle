using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class Enemy : Character
{
    [SerializeField] private SpriteRenderer skin;
    private EnemyStats _stats;

    public EnemyStats CurrentStats => _stats;

    public event Action<Item> OnDropItem;
    protected override void Awake()
    {
        base.Awake();
        GetComponent<Health>().IsDead += DeadEnemy;
        gameObject.SetActive(false);
    }

    private void DeadEnemy()
    {
        var dropList = CurrentStats.ItemDropList;
        float randomValue = Random.value;
        float totalChance = 0f;

        foreach (var item in dropList)
        {
            totalChance += item.ItemStats.ChanceToDrop;
        }

        float cumulativeChance = 0f;
        foreach (var item in dropList)
        {
            cumulativeChance += item.ItemStats.ChanceToDrop / totalChance;

            if (randomValue < cumulativeChance)
            {
                OnDropItem?.Invoke(item);
                break;
            }
        }
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
        GetComponent<Health>().IsDead -= DeadEnemy;
    }
}
