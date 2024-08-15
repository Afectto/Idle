using System;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [SerializeField]private WeaponStats _stats;
    public event Action OnChangeWeaponStats; 

    public WeaponStats GetWeaponStats()
    {
        return _stats;
    }
    
    public void ChangeWeapon(WeaponStats weaponStats)
    {
        _stats = weaponStats;
        OnChangeWeaponStats?.Invoke();
    }
}
