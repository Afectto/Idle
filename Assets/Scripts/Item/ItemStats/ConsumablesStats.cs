using System;
using UnityEngine;

[Serializable]
[CreateAssetMenu(fileName = "ConsumablesStats", menuName = "GameInfo/ItemStats/New ConsumablesStats")]
public class ConsumablesStats : ItemStats
{
    public float HealthRestored;
    
    public override void ApplyItem(Player target)
    {
        target.ChangeStat(StatsType, HealthRestored);
    }

    public override void RemoveItem(Player target)
    {
    }
    
    public override string GetStatsText()
    {
        var text = base.GetStatsText();
        text += $"\nRestores: {HealthRestored}";
        
        return text;
    }
}