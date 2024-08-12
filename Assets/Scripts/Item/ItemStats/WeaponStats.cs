using System;
using UnityEngine;
using UnityEngine.Serialization;

[Serializable]
[CreateAssetMenu(fileName = "WeaponStats", menuName = "GameInfo/ItemStats/New WeaponStats")]
public class WeaponStats : ItemStats
{
    public float Damage;
    public float AttackDuration;
    
    public override void ApplyItem(Player target)
    {
        target.ChangeStat(Type, Damage);
    }

    public override void RemoveItem(Player target)
    {
        target.ChangeStat(Type, Damage);
    }
}