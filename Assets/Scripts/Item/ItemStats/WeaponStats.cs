using System;
using UnityEngine;

[Serializable]
[CreateAssetMenu(fileName = "WeaponStats", menuName = "GameInfo/ItemStats/New WeaponStats")]
public class WeaponStats : ItemStats
{
    public float Damage;
    
    public override void ApplyItem()
    {
        
    }
}