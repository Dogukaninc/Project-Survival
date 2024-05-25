using Survival_Swarm_Files.Scripts;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    private readonly TimerTicker timerTicker = new TimerTicker();
    
    [Header(" Spawner Settings ")]
    [SerializeField] private int spawnCount;
    [SerializeField] private Enemy enemyToSpawn;

    void Start()
    {
        
    } 

    void Update()
    {
        
    }

    private void SpawnEnemy()
    {
        //Her interval time arasında yeni bir enemy prefab'i spawnla
        
    }
    
}
