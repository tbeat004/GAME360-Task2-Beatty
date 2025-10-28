using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float jumpForce = 7f;

    private Rigidbody rb;
    private bool isGrounded = false;

    public GameObject bulletPrefab;   
    public Transform firePoint;       
    public float fireRate = 0.25f;

    private float nextFire = 0f;
    private StateMachine stateMachine;
    
    // Store state instances
    public IdleState idleState;
    public MovingState movingState;
    public JumpingState jumpingState;
    public AttackingState attackingState;
    
    // Called when script instance is being loaded
    void Awake()
    {
        // Get the Rigidbody component attached to this GameObject
        rb = GetComponent<Rigidbody>();
        stateMachine = GetComponent<StateMachine>();
        
        // Verify components were found
        if (rb == null)
        {
            Debug.LogError("No Rigidbody found on Player!");
        }
        else
        {
            Debug.Log("PlayerController Awake - Rigidbody initialized");
        }
        
        if (stateMachine == null)
        {
            Debug.LogError("No StateMachine found on Player!");
        }
        
        // Create state instances ONCE
        idleState = new IdleState(stateMachine, this);
        movingState = new MovingState(stateMachine, this);
        jumpingState = new JumpingState(stateMachine, this);
        attackingState = new AttackingState(stateMachine, this);
    }
    
    // Called before the first frame update
    void Start()
    {
        // Initialize with Idle state
        stateMachine.ChangeState(idleState);
    }
    
    // Called once per frame
    void Update()
    {
        // State machine now handles calling the appropriate methods
        // Remove manual calls - the state Execute() will call them
        // HandleMovement();
        // HandleJumping();
        // HandleShooting();
    }
    
    public void HandleMovement()
    {
        float horizontal = Input.GetAxisRaw("Horizontal"); 
        float vertical = Input.GetAxisRaw("Vertical");

        Vector3 movement = new Vector3(horizontal, 0f, vertical).normalized;

        if (movement.magnitude > 0f)
        {
            transform.Translate(movement * moveSpeed * Time.deltaTime, Space.Self);
        }
    }
    
    public void HandleJumping()
    {
        // Check for jump input
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            Debug.Log("Player jumped!");
        }
    }

    public void HandleShooting()
    {
        if (Input.GetMouseButton(0) && Time.time >= nextFire) 
        {
            nextFire = Time.time + fireRate;

            // pick spawn point
            Vector3 spawnPos = firePoint ? firePoint.position : transform.position + transform.forward * 1f;

            Instantiate(bulletPrefab, spawnPos, transform.rotation);
            AudioManager.Instance?.PlaySFX(AudioManager.Instance.shootSFX);

            Debug.Log("Bullet fired!");
        }
    }
    
    // Public getter for isGrounded so states can check it
    public bool IsGrounded()
    {
        return isGrounded;
    }
   
    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
            Debug.Log("Player landed on ground");
        }
    }
    
    void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = false;
            Debug.Log("Player left ground");
        }
    }
}
