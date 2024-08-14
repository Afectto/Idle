using System;
using UnityEngine;

[Serializable]
public abstract class ItemStats : ScriptableObject
{
    [SerializeField] private StatType statType;
    public StatType StatsType => statType;
    
    public abstract void ApplyItem(Player target);
    public abstract void RemoveItem(Player target);
}