using System;
using UnityEngine;

public class Enemy : Character
{
    [SerializeField] private EnemyStats stats;
    [SerializeField] private SpriteRenderer skin;

    public EnemyStats CurrentStats => stats;
    private Player _target;

    protected override void Start()
    {
        base.Start();
        skin.sprite = stats.Skin;
        _target = FindObjectOfType<Player>();
        StateMachine.SetCommonParameters(stats.Stats.TimeToPrepareAttack, GetComponent<Weapon>().GetWeaponStats(), _target);
    }

    public override Stats GetCurrentStats()
    {
        return stats.Stats;
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
    
    private void StartFight()
    {
        if (_target)
        {
            StateMachine.ChangeState(new PrepareToFightState(StateMachine));
        }
    }
}
