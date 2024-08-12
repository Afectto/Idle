using UnityEngine;

public class Weapon : MonoBehaviour
{
    private Player _player;
    private WeaponStats _stats;

    public WeaponStats GetWeaponStats()
    {
        return _stats;
    }
    
    public void ChangeWeapon(WeaponStats weaponStats)
    {
        _stats = weaponStats;
    }
}
