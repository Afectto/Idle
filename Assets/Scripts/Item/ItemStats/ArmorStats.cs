using System;
using UnityEngine;

[Serializable]
[CreateAssetMenu(fileName = "ArmorStats", menuName = "GameInfo/ItemStats/New ArmorStats")]
public class ArmorStats : ItemStats
{
    public float Defense;
    
    public override void ApplyItem(Player target)
    {
        target.ChangeStat(StatsType, Defense);
    }

    public override void RemoveItem(Player target)
    {
        target.ChangeStat(StatsType, -Defense);
    }

    public override string GetStatsText()
    {
        var text = base.GetStatsText();
        text += $"\nArmor: {Defense}";
        
        return text;
    }
}