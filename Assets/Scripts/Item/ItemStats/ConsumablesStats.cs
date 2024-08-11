﻿using System;
using UnityEngine;

[Serializable]
[CreateAssetMenu(fileName = "ConsumablesStats", menuName = "GameInfo/ItemStats/New ConsumablesStats")]
public class ConsumablesStats : ItemStats
{
    public float HealthRestored;
    
    public override void ApplyItem()
    {
        
    }
}