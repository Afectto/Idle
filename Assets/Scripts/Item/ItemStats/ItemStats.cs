using System;
using UnityEngine;

[Serializable]
public abstract class ItemStats : ScriptableObject
{
    [SerializeField] private StatType statType;
    [SerializeField] public WearableItemsType type;
    public StatType StatsType => statType;
    public WearableItemsType WearableType => type;
    
    public abstract void ApplyItem(Player target);
    public abstract void RemoveItem(Player target);
}