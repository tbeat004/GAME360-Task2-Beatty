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
    
    // Called when script instance is being loaded
    void Awake()
    {
        // Get the Rigidbody component attached to this GameObject
        rb = GetComponent<Rigidbody>();
        
        // Verify component was found
        if (rb == null)
        {
            Debug.LogError("No Rigidbody found on Player!");
        }
        else
        {
            Debug.Log("PlayerController Awake - Rigidbody initialized");
        }
    }
    
    // Called before the first frame update
    void Start()
    {

    }
    
    // Called once per frame
    void Update()
    {
        HandleMovement();
        HandleJumping();
        HandleShooting();

    }
    
    void HandleMovement()
    {
        // // Get input from keyboard
        // float horizontal = Input.GetAxis("Horizontal"); // A/D or Left/Right arrows
        // float vertical = Input.GetAxis("Vertical");     // W/S or Up/Down arrows
        
        // // Create movement vector
        // Vector3 movement = new Vector3(horizontal, 0f, vertical);
        // movement = movement.normalized * moveSpeed * Time.deltaTime;
        
        // // Apply movement
        // transform.Translate(movement, Space.Self);
    float horizontal = Input.GetAxisRaw("Horizontal"); 
    float vertical = Input.GetAxisRaw("Vertical");

    Vector3 movement = new Vector3(horizontal, 0f, vertical).normalized;

    if (movement.magnitude > 0f)
    {
        transform.Translate(movement * moveSpeed * Time.deltaTime, Space.Self);
    }
    }
    
    void HandleJumping()
    {
        // Check for jump input
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            Debug.Log("Player jumped!");
        }
    }

    void HandleShooting()
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
