using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "EnemyStats", menuName = "GameInfo/New EnemyStats")]
public class EnemyStats : ScriptableObject
{
    [SerializeField] private Stats stats;
    [SerializeField] private GameObject enemyPrefab;
    [SerializeField, Range(0,1)] private float chanceToSpawn;
    [SerializeField] private float exp;
    [SerializeField] private List<Item> itemDropList;

    public Stats Stats => stats;
    public GameObject EnemyPrefab => enemyPrefab;
    public float ChanceToSpawn => chanceToSpawn;
    public float Exp => exp;
    public List<Item> ItemDropList => itemDropList;
}