using System;
using UnityEngine;

[Serializable]
public abstract class ItemStats : ScriptableObject
{
    public abstract void ApplyItem();
}