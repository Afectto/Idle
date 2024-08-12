using UnityEngine;

[RequireComponent(typeof(Enemy))]
public class EnemyHealth : Health
{
    private Enemy _enemy;
    
    void Start()
    {
        _enemy = GetComponent<Enemy>();
        Initialize(_enemy.CurrentStats.Stats.Health);
    }

}
