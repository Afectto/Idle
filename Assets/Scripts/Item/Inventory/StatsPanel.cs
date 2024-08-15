using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class StatsPanel : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI text;
    [SerializeField] private Player player;
    private WeaponStats _weaponStats;
    private float _health;
    private float _armor;
    private float _attackPower;
    private float _timeToPrepareAttack;
    private float _luck;

    private void Start()
    {
        _weaponStats = player.GetComponent<Weapon>().GetWeaponStats();
        var stats = player.GetCurrentStats();
        _health = stats.Health;
        _armor = stats.Armor;
        _attackPower = stats.AttackPower + _weaponStats.Damage;
        _timeToPrepareAttack = stats.TimeToPrepareAttack;
        _luck = stats.Luck;
        UpdateText();
        player.StatChange += ChangeStats;
        player.GetComponent<Weapon>().OnChangeWeaponStats += OnChangeWeapon;
    }

    private void OnChangeWeapon()
    {
        var stats = player.GetCurrentStats();
        _weaponStats = player.GetComponent<Weapon>().GetWeaponStats();
        _attackPower = stats.AttackPower + _weaponStats.Damage;
        UpdateText();
    }

    private void ChangeStats(StatType statType, float value)
    {
        switch (statType)
        {
            case StatType.Health:
                _health += value;
            break;
            case StatType.Armor:
                _armor += value;
            break;
            case StatType.AttackPower:
                _attackPower += value;
            break;
            case StatType.TimeToPrepareAttack:
                _timeToPrepareAttack += value;
            break;
            case StatType.Luck:
                _luck += value;
            break;
        }

        UpdateText();
    }

    private void UpdateText()
    {
        float fontSize = text.fontSize;
        text.text = $"<size={fontSize}><color=white><align=center>Player Stats:</align></color></size>\n" +
                    $"<color=yellow>Health:</color> {_health}\n" +
                    $"<color=yellow>Armor:</color> {_armor}\n" +
                    $"<color=yellow>Attack Power:</color> {_attackPower}\n" +
                    $"<size={fontSize-1.5f}><color=yellow>Time To Prepare Attack:</color> {_timeToPrepareAttack}</size>\n" +
                    $"<color=yellow>Luck:</color> {_luck}";
    }

    private void OnDestroy()
    {
        if (player)
        {
            player.StatChange -= ChangeStats;
            player.GetComponent<Weapon>().OnChangeWeaponStats -= OnChangeWeapon;
        }
    }
}
