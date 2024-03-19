using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Basic_Spawner : MonoBehaviour
{
    public GameObject humanPrefab; // Ýnsan prefab'ý
    public Transform target; // Hedef konum
    public float spawnInterval = 5f; // Spawn aralýðý (saniye)
    public float spawnRadius = 5f; // Spawn yarýçapý

    private void Start()
    {
        // SpawnCoroutine coroutine'ini baþlat
        StartCoroutine(SpawnCoroutine());
    }

    private IEnumerator SpawnCoroutine()
    {
        while (true)
        {
            // Belirli aralýklarla spawn et
            SpawnHuman();
            yield return new WaitForSeconds(spawnInterval);
        }
    }

    private void SpawnHuman()
    {
        // Rastgele bir noktada insaný spawn et
        Vector3 spawnPosition = Random.insideUnitSphere * spawnRadius + transform.position;
        spawnPosition.y = 0f; // Yüksekliði sýfýra ayarla

        // Insaný spawn et ve hedef konumu ayarla
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
