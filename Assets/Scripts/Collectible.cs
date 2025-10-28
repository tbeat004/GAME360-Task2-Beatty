using UnityEngine;

public class Collectible : MonoBehaviour
{
    [Header("Collectible Settings")]
    public int scoreValue = 10;
    public float rotationSpeed = 50f;

    public CoinSpawner spawner;
    
    void Start()
    {
        Debug.Log("Collectible created: " + gameObject.name + " worth " + scoreValue + " points");
    }
    
    void Update()
    {
        // Rotate the collectible for visual appeal
        transform.Rotate(Vector3.up * rotationSpeed * Time.deltaTime);
    }
    
    // Called when another collider enters this trigger collider
    void OnTriggerEnter(Collider other)
    {
        // Check if the player touched this collectible
        if (other.CompareTag("Player"))
        {
            // Get the PlayerController component
            PlayerController player = other.GetComponent<PlayerController>();
            
            if (player != null)
            {
                // Add score to player through a game manager
                GameManager.Instance?.AddScore(scoreValue);
                EventManager.Instance.TriggerEvent(GameEvents.onCollectibleCollected);

                spawner.SpawnCoin();
                
                // Log collection
                Debug.Log("COLLECTED: " + gameObject.name + " for " + scoreValue + " points!");
                
                // Destroy this collectible
                Destroy(gameObject);
            }
        }
    }
}
