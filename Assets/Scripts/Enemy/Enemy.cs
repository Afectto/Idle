using System;
using UnityEngine;

public class Enemy : Character
{
    [SerializeField] private EnemyStats stats;
    public Action<float, float> HealthChange;

    protected override void Start()
    {
        base.Start();
    }
    
}
