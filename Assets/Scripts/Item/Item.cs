﻿using UnityEngine;

[CreateAssetMenu(fileName = "Item", menuName = "GameInfo/ItemStats/New Item")]
public class Item : ScriptableObject
{
    [SerializeField] private Sprite skin;
    [SerializeReference] private ItemStats _stats;

    public Sprite GetSkin() => skin;
    
    public void ApplyItem(Player target)
    {
        _stats.ApplyItem(target);
    }
    
    public void RemoveItem(Player target)
    {
        _stats.RemoveItem(target);
    }
}