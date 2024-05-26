using System;
using Survival_Swarm_Files.Scripts;
using Unity.Mathematics;
using UnityEngine;

// TODO Enemy öldükten 2 saniye sonra pool'a ageri dönecek.
// Enemy pool'u sabit bir degere sahip olsun (100 adet gibi) ancak her el spawn olacak enemy sayısı değişecek o ayrı.
// Enemy spawn pointleri her elde map prefabine göre ayarlanacak
public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private WaveManager waveManager;

    private readonly TimerTicker timerTicker = new TimerTicker();

    [Header(" Spawner Settings ")] [Space(5)] [SerializeField]
    private int enemySpawnCount;

    [SerializeField] private Transform spawnPoint;
    [SerializeField] private float spawnIntervalTime;

    private float spawnDefaultTimeValue;

    public Action startSpawner;
    public Action spawnEnemy;

    private bool canSpawnerWork;
    [SerializeField] private int spawnedEnemyCount;

    private void OnEnable()
    {
        startSpawner += SetSpawnerWorkState;
        spawnEnemy += SpawnEnemy;
    }

    private void OnDisable()
    {
        startSpawner -= SetSpawnerWorkState;
        spawnEnemy -= SpawnEnemy;
    }

    private void Start()
    {
        spawnDefaultTimeValue = spawnIntervalTime;
    }

    void Update()
    {
        if (spawnedEnemyCount < waveManager.TOTALENEMYSTOSPAWN)
        {
            if (canSpawnerWork && spawnedEnemyCount < enemySpawnCount)
            {
                timerTicker.SpawnEnemyInterval(ref spawnIntervalTime, spawnDefaultTimeValue, spawnEnemy);
            }
            else
            {
                canSpawnerWork = false;
                spawnedEnemyCount = 0;
            }
        }

        
    }

    private void SetSpawnerWorkState()
    {
        canSpawnerWork = true;
    }

    public void SpawnEnemy()
    {
        ObjectPooler.Instance.SpawnFromPool("Enemy", spawnPoint.position, quaternion.identity);
        spawnedEnemyCount++;
    }
}