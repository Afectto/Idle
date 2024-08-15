using System;
using UnityEngine;

[Serializable]
public abstract class ItemStats : ScriptableObject
{
    [SerializeField] private StatType statType;
    [SerializeField] private WearableItemsType type;
    [SerializeField, Range(0,1)] private float chanceToDrop;
    [SerializeField] private bool isStackable;
    
    public StatType StatsType => statType;
    public WearableItemsType WearableType => type;    
    public float ChanceToDrop => chanceToDrop;
    public bool IsStackable => isStackable;
    
    public abstract void ApplyItem(Player target);
    public abstract void RemoveItem(Player target);

    public virtual string GetStatsText()
    {
        return $"Is Stackable: {isStackable}";
    }
}