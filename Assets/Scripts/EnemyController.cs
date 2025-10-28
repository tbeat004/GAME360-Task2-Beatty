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
    private Vector3 strafeAxis;      
    private Vector3 targetPos;
    private bool moving;

    void Awake()
    {
        health = maxHealth;
    }

    void Start()
    {
        startPos = transform.position;

        strafeAxis = transform.right;
        strafeAxis.y = 0f;
        if (strafeAxis.sqrMagnitude < 0.0001f) strafeAxis = Vector3.right; 
        strafeAxis.Normalize();

        ChooseNewTarget();
    }

    void Update()
    {
        if (!moving) return;

        transform.position = Vector3.MoveTowards(transform.position, targetPos, moveSpeed * Time.deltaTime);

        if ((transform.position - targetPos).sqrMagnitude <= 0.0025f) 
        {
            moving = false;
            Invoke(nameof(ChooseNewTarget), pauseTime);
        }
    }

    void ChooseNewTarget()
    {
        if (maxRange <= 0f) return;


        float offset = Random.Range(-maxRange, maxRange);
        targetPos = startPos + strafeAxis * offset;
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
        EventManager.Instance.TriggerEvent(GameEvents.onEnemyDefeated);
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

