using UnityEngine;

public class CoinSpawner : MonoBehaviour
{
    public Transform[] spawnPoints;   
    public GameObject coinPrefab;     
    private GameObject currentCoin;

    void Start()
    {
        SpawnCoin();
    }

    public void SpawnCoin()
    {
        
        int index = Random.Range(0, spawnPoints.Length);
        Transform spawn = spawnPoints[index];

       
        currentCoin = Instantiate(coinPrefab, spawn.position, spawn.rotation);
        
        currentCoin.GetComponent<Collectible>().spawner = this;
    }
}

