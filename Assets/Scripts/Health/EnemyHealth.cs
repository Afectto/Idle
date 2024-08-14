using UnityEngine;

[RequireComponent(typeof(Enemy))]
public class EnemyHealth : Health
{
    private Enemy _enemy;
    
    void Start()
    {
        _enemy = GetComponent<Enemy>();
        if (_enemy.CurrentStats)
        {
            Initialize(_enemy.CurrentStats.Stats.Health);
        }
    }

}
