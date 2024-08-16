using System;

[Serializable]
public struct Stats
{
    public float Health;
    public float Armor;
    public float AttackPower;
    public float TimeToPrepareAttack;
    public float Luck;
    
    public float GetTotalStats()
    {
        return Health + Armor + AttackPower + TimeToPrepareAttack + Luck;
    }
}