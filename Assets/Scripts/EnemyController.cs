using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public int maxHealth = 1;
    public int scoreOnDeath = 50;

    public float maxRange = 5f;      
    public float moveSpeed = 2f;     
    public float pauseTime = 0.5f;   

    private int health;
    private Vector3 startPos;
    private Vector3 targetPos;
    private bool moving = false;

    void Awake()
    {
        health = maxHealth;
    }

    void Start()
    {
        startPos = transform.position;
        ChooseNewTarget();
    }

    void Update()
    {
        if (moving)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPos, moveSpeed * Time.deltaTime);

            if (Vector3.Distance(transform.position, targetPos) < 0.01f)
            {
                moving = false;
                Invoke(nameof(ChooseNewTarget), pauseTime);
            }
        }
    }

    void ChooseNewTarget()
    {
        
        float newX = Random.Range(-maxRange, maxRange);
        targetPos = new Vector3(startPos.x + newX, startPos.y, startPos.z);

        moving = true;
    }

    public void TakeDamage(int amount)
    {
        health -= amount;
        if (health <= 0) Die();
    }

    private void Die()
    {
        GameManager.Instance?.AddScore(scoreOnDeath);
        Destroy(gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Bullet"))
        {
            TakeDamage(1);
            Destroy(other.gameObject);
        }
    }
}

