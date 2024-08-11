using System;
using UnityEngine;

[Serializable]
[CreateAssetMenu(fileName = "ArmorStats", menuName = "GameInfo/ItemStats/New ArmorStats")]
public class ArmorStats : ItemStats
{
    public float Defense;
    
    public override void ApplyItem()
    {
        
    }
}