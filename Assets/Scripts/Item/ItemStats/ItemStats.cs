using System;
using UnityEngine;

[Serializable]
public abstract class ItemStats : ScriptableObject
{
    [SerializeField] private StatType type;
    public StatType Type => type;
    
    public abstract void ApplyItem(Player target);
    public abstract void RemoveItem(Player target);
}