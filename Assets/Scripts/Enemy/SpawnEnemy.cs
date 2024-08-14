using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

public class SpawnEnemy : MonoBehaviour
{
    [SerializeField] private FightController fightController;
    private Enemy _enemyObject;
    private List<EnemyStats> _listEnemyStats;

    private void Awake()
    {
        _listEnemyStats = Resources.LoadAll<EnemyStats>("Enemy").ToList();
        fightController.OnStartFight += StartFight;
    }

    private void StartFight(Enemy obj)
    {
        _enemyObject = obj;
        _enemyObject.GetComponent<Health>().IsDead += TrySpawnEnemy;
        TrySpawnEnemy();
    }
    
    private void TrySpawnEnemy()
    {
        float randomValue = Random.value;
        float totalChance = 0f;

        foreach (var enemy in _listEnemyStats)
        {
            totalChance += enemy.ChanceToSpawn;
        }

        float cumulativeChance = 0f;
        foreach (var enemy in _listEnemyStats)
        {
            cumulativeChance += enemy.ChanceToSpawn / totalChance;

            if (randomValue < cumulativeChance)
            {
                SpawnEnemyByStats(enemy);
                break;
            }
        }
    }

    private void SpawnEnemyByStats(EnemyStats stats)
    {
        _enemyObject.gameObject.SetActive(true);
        _enemyObject.SetNewStats(stats);
        
    }
}
