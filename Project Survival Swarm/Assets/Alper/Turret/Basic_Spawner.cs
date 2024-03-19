using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Basic_Spawner : MonoBehaviour
{
    public GameObject humanPrefab; // �nsan prefab'�
    public Transform target; // Hedef konum
    public float spawnInterval = 5f; // Spawn aral��� (saniye)
    public float spawnRadius = 5f; // Spawn yar��ap�

    private void Start()
    {
        // SpawnCoroutine coroutine'ini ba�lat
        StartCoroutine(SpawnCoroutine());
    }

    private IEnumerator SpawnCoroutine()
    {
        while (true)
        {
            // Belirli aral�klarla spawn et
            SpawnHuman();
            yield return new WaitForSeconds(spawnInterval);
        }
    }

    private void SpawnHuman()
    {
        // Rastgele bir noktada insan� spawn et
        Vector3 spawnPosition = Random.insideUnitSphere * spawnRadius + transform.position;
        spawnPosition.y = 0f; // Y�ksekli�i s�f�ra ayarla

        // Insan� spawn et ve hedef konumu ayarla
        GameObject human = Instantiate(humanPrefab, spawnPosition, Quaternion.identity);
        NavMeshAgent navMeshAgent = human.GetComponent<NavMeshAgent>();
        if (navMeshAgent != null && target != null)
        {
            navMeshAgent.SetDestination(target.position);
        }
        else
        {
            Debug.LogWarning("NavMeshAgent veya hedef konum eksik.");
        }
    }
}
