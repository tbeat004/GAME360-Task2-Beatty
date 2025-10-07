using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemyPrefab;     
    public Transform[] spawnPoints;    
    private GameObject currentEnemy;

    void Start()
    {
        SpawnEnemy();
    }

    void Update()
    {
        
        if (currentEnemy == null)
        {
            SpawnEnemy();
        }
    }

    void SpawnEnemy()
    {
        if (spawnPoints.Length == 0 || enemyPrefab == null) return;

        int index = Random.Range(0, spawnPoints.Length);
        Transform spawn = spawnPoints[index];

        currentEnemy = Instantiate(enemyPrefab, spawn.position, spawn.rotation);
    }
}
