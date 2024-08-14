using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class FightController : MonoBehaviour
{
    [SerializeField] private Button button;
    [SerializeField] private Player player;
    [SerializeField] private Enemy enemy;

    public event Action<Enemy> OnStartFight; 
    private void Awake()
    {
        button.onClick.AddListener(StartFight);
        player.IsDead += StopFight;
    }

    private void StartFight()
    {
        OnStartFight?.Invoke(enemy);
        button.onClick.RemoveListener(StartFight);
        button.onClick.AddListener(StopFight);
        button.GetComponentInChildren<TextMeshProUGUI>().text = "Stop Fight";
        player.StartFight();
        enemy.StartFight();
    }

    private void StopFight()
    {
        button.onClick.RemoveListener(StopFight);
        button.onClick.AddListener(StartFight);
        button.GetComponentInChildren<TextMeshProUGUI>().text = "Start Fight";
        player.OutOfFight();
        enemy.OutOfFight();
    }
}
