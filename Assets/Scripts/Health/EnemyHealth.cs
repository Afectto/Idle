using UnityEngine;

[RequireComponent(typeof(Enemy))]
public class EnemyHealth : Health
{
    private Enemy _enemy;
    
    void Start()
    {
        _enemy = GetComponent<Enemy>();
        _enemy.HealthChange += ChangeSlider;
    }

    private void OnDestroy()
    {
        _enemy.HealthChange -= ChangeSlider;
    }
}
