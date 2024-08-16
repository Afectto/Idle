using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerLevel : MonoBehaviour
{
    [SerializeField]private Player player;
    [SerializeField] private Image indicatorFill;
    [SerializeField] private TextMeshProUGUI textLevel;
    [SerializeField] private TextMeshProUGUI pointLeft;

    [Space]
    [SerializeField] private Button showLevelStats;
    [SerializeField] private GameObject panelStats;
    
    [Space]
    [SerializeField] private TextMeshProUGUI health;
    [SerializeField] private TextMeshProUGUI armor;
    [SerializeField] private TextMeshProUGUI attackPower;
    [SerializeField] private TextMeshProUGUI timeToPrepareAttack;
    [SerializeField] private TextMeshProUGUI luck;
    
    private Stats _stats;
    private float _level;
    private float _experience;
    private float _experienceToNextLevel;
    private SavePlayerLevel _savePlayerLevel;

    public void Start()
    {
        _level = 1;
        _experience = 0;
        _experienceToNextLevel = 10;
        EarnExperience(0);
        textLevel.text = _level.ToString();
        player.OnTargetDead += TargetDead;
        showLevelStats.onClick.AddListener(ChangeShowStats);
        panelStats.gameObject.SetActive(false);
        
        _savePlayerLevel = new SavePlayerLevel(this);
        _savePlayerLevel.LoadPlayerLevelStats();
        
        UpdateTextStats();
    }

    public PlayerLvlStats GetLevelStats()
    {
        return new PlayerLvlStats(_stats, _level, _experience, _experienceToNextLevel);
    }

    private void ChangeShowStats()
    {
        panelStats.gameObject.SetActive(!panelStats.activeSelf);
    }

    private void TargetDead(EnemyStats stats)
    {
        EarnExperience(stats.Exp);
    }

    private void EarnExperience(float amount)
    {
        _experience += amount;

        while (_experience >= _experienceToNextLevel)
        {
            LevelUp();
        }

        indicatorFill.fillAmount = _experience / _experienceToNextLevel;
    }

    private void LevelUp()
    {
        _experience -= _experienceToNextLevel;
        _level++;
        
        _experienceToNextLevel = CalculateExperienceToNextLevel(_level);
        textLevel.text = _level.ToString();
        UpdatePointLeft();
    }

    private float CalculateExperienceToNextLevel(float currentLevel)
    {
        return 10 * Mathf.Pow(currentLevel, 2);
    }

    public void ChangeStat(string nameStat)
    {
        if (GetSkillPointCount() > 0)
        {
            Enum.TryParse(nameStat, out StatType statType);
            ChangeStat(statType);
        }
    }
    
    public void ChangeStat(StatType statType, float value = 1)
    {
        switch (statType)
        {
            case StatType.Health:
                player.ChangeStat(StatType.Health, -_stats.Health);
                _stats.Health += 5;
                player.ChangeStat(StatType.Health, _stats.Health);
                break;
            case StatType.Armor:
                player.ChangeStat(StatType.Armor, -_stats.Armor);
                _stats.Armor += value;
                player.ChangeStat(StatType.Armor, _stats.Armor);
                break;
            case StatType.AttackPower:
                player.ChangeStat(StatType.AttackPower, -_stats.AttackPower);
                _stats.AttackPower += value;
                player.ChangeStat(StatType.AttackPower, _stats.AttackPower);
                break;
            case StatType.TimeToPrepareAttack:
                player.ChangeStat(StatType.TimeToPrepareAttack, +_stats.TimeToPrepareAttack);
                _stats.TimeToPrepareAttack += value/20;
                player.ChangeStat(StatType.TimeToPrepareAttack, -_stats.TimeToPrepareAttack);
                break;
            case StatType.Luck:
                player.ChangeStat(StatType.Luck, -_stats.Luck);
                _stats.Luck += value;
                player.ChangeStat(StatType.Luck, _stats.Luck);
                break;
        }

        UpdateTextStats();
    }

    private void UpdateTextStats()
    {
        health.text = _stats.Health.ToString();
        armor.text = _stats.Armor.ToString();
        attackPower.text = _stats.AttackPower.ToString();
        timeToPrepareAttack.text = _stats.TimeToPrepareAttack.ToString("F2");
        luck.text = _stats.Luck.ToString();
        UpdatePointLeft();
        
        textLevel.text = _level.ToString();
        indicatorFill.fillAmount = _experience / _experienceToNextLevel;
        
        _savePlayerLevel.SavePlayerLevelStats();
    }

    public void UpdatePointLeft()
    {
        pointLeft.text = "Point Left: " + GetSkillPointCount();
    }
    
    public int GetSkillPointCount()
    {
        return (int)Math.Ceiling(_level - (_stats.GetTotalStats() - _stats.TimeToPrepareAttack + _stats.TimeToPrepareAttack * 20 - _stats.Health + _stats.Health / 5));
    }
    
    public void OnDestroy()
    {
        _savePlayerLevel.SavePlayerLevelStats();
        player.OnTargetDead += TargetDead;
    }

    public void UpdateStats(PlayerLvlStats stats)
    {
        _stats = stats.Stats;
        _level = stats.Level;
        _experience = stats.Experience;
        _experienceToNextLevel = stats.ExperienceToNextLevel;
        
        
        player.ChangeStat(StatType.Health, _stats.Health);
        player.ChangeStat(StatType.Armor, _stats.Armor);
        player.ChangeStat(StatType.AttackPower, _stats.AttackPower);
        player.ChangeStat(StatType.TimeToPrepareAttack, _stats.TimeToPrepareAttack);
        player.ChangeStat(StatType.Luck, _stats.Luck);
        UpdateTextStats();
    }
}
