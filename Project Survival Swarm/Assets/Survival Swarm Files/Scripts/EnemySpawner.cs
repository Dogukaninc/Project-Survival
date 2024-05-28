using System;
using Survival_Swarm_Files.Scripts;
using Unity.Mathematics;
using System.Collections;
using UnityEngine;
using UnityEngine.AI;

// TODO Enemy öldükten 2 saniye sonra pool'a ageri dönecek.
// Enemy pool'u sabit bir degere sahip olsun (100 adet gibi) ancak her el spawn olacak enemy sayısı değişecek o ayrı.
// Enemy spawn pointleri her elde map prefabine göre ayarlanacak
public class EnemySpawner : MonoBehaviour
{
    public GameObject enemyPrefab;
    [SerializeField] private WaveManager waveManager;

    private readonly TimerTicker timerTicker = new TimerTicker();

    [Header(" Spawner Settings ")]
    [Space(5)]
    [SerializeField]
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
        GameObject enemyObject = Instantiate(enemyPrefab, spawnPoint.position, Quaternion.identity);
        spawnedEnemyCount++;
        StartCoroutine(InitializeEnemy(enemyObject));
    }


    private IEnumerator InitializeEnemy(GameObject enemyObject)
    {
        NavMeshAgent agent = enemyObject.GetComponent<NavMeshAgent>();
        Enemy enemy = enemyObject.GetComponent<Enemy>();

        yield return new WaitForEndOfFrame(); // Bir frame bekleyerek NavMesh'in güncellenmesini bekleyin

        // Eğer agent NavMesh'te değilse, en yakın NavMesh noktasına taşıyın
        while (agent == null || !agent.isOnNavMesh)
        {
            if (agent != null && !agent.isOnNavMesh)
            {
                NavMeshHit hit;
                if (NavMesh.SamplePosition(agent.transform.position, out hit, 1.0f, NavMesh.AllAreas))
                {
                    agent.Warp(hit.position); // Agent'ı en yakın NavMesh noktasına taşı
                }
            }
            yield return null;
        }

        if (enemy != null && agent != null)
        {
            enemy.stateMachine.ChangeState(new EnemyNavState(enemy, agent, enemy.mainTarget.transform));
        }
        else
        {
            Debug.LogWarning("Enemy or NavMeshAgent component not found on the spawned object.");
        }
    }

}