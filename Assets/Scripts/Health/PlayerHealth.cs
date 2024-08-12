using UnityEngine;

[RequireComponent(typeof(Player))]
public class PlayerHealth : Health
{
    private Player _player;
    
    void Start()
    {
        _player = GetComponent<Player>();
        _player.HealthChange += ChangeSlider;
    }

    private void OnDestroy()
    {
        _player.HealthChange -= ChangeSlider;
    }
}
