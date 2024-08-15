using System;
using UnityEngine;

public enum WeaponType
{
    Melee,
    Range
}

[Serializable]
[CreateAssetMenu(fileName = "WeaponStats", menuName = "GameInfo/ItemStats/New WeaponStats")]
public class WeaponStats : ItemStats
{
    public WeaponType weaponType;
    public float Damage;
    public float AttackDuration;
    
    public override void ApplyItem(Player target)
    {
        target.ChangeWeapon(this);
    }

    public override void RemoveItem(Player target)
    {
        target.ChangeWeapon(new WeaponStats
        {
            weaponType = WeaponType.Melee,
            Damage = 0,
            AttackDuration = 0.5f
        });
    }
}