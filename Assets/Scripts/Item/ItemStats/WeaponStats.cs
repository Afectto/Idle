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
        target.ChangeStat(StatsType, Damage);
    }

    public override void RemoveItem(Player target)
    {
        target.ChangeStat(StatsType, Damage);
    }
}