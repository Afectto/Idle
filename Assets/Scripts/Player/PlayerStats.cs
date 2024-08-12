using UnityEngine;

[CreateAssetMenu(fileName = "PlayerStats", menuName = "GameInfo/New PlayerStats")]
public class PlayerStats : ScriptableObject
{
    [SerializeField] private Stats stats;
    
    public Stats Stats => stats;
    
}