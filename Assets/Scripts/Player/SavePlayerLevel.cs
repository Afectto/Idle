using System.IO;
using UnityEngine;

public struct PlayerLvlStats
{
    public Stats Stats;
    public float Level;
    public float Experience;
    public float ExperienceToNextLevel;

    public PlayerLvlStats(Stats stats,float level,float experience,float experienceToNextLevel)
    {
        Stats = stats;
        Level = level;
        Experience = experience;
        ExperienceToNextLevel = experienceToNextLevel;
    }
}

public class SavePlayerLevel
{
    private string inventoryPlayerFilePath = "Assets/SaveFilePlayerLevel.txt";
    private readonly PlayerLevel _playerLevel;

    public SavePlayerLevel(PlayerLevel playerLevel)
    {
        _playerLevel = playerLevel;
    }

    public void SavePlayerLevelStats()
    {
        string json = JsonUtility.ToJson(_playerLevel.GetLevelStats());
        File.WriteAllText(inventoryPlayerFilePath, json);
    }

    public void LoadPlayerLevelStats()
    {
        if (File.Exists(inventoryPlayerFilePath))
        {
            string json = File.ReadAllText(inventoryPlayerFilePath);
            PlayerLvlStats stats = JsonUtility.FromJson<PlayerLvlStats>(json);
            _playerLevel.UpdateStats(stats);
        }
    }


}
