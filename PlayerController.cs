using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 10f;
    public float jumpForce = 10f;
    public float groundDistance = 0.5f;
    public float lookSpeed = 12f;
    public float fallDamageThreshold = 5f;
    public int maxHealth = 100;
    public int fallDamageAmount = 10;
    private bool isGrounded;
    private int currentHealth;
    private Vector3 lastPosition; // Store the player's position in the previous frame

    private Rigidbody rb;
    private float verticalRotation = 0f;
    private bool isCursorLocked = true;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;

        Cursor.lockState = CursorLockMode.Locked;
        currentHealth = maxHealth;
        lastPosition = transform.position; // Initialize lastPosition
    }

    private void Update()
    {
        MovePlayer();
        Jump();
        LookAround();
        LockUnlockCursor();
        ApplyFallDamage();
        lastPosition = transform.position; // Update lastPosition at the end of Update
    }

    private void MovePlayer()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        Vector3 moveDirection = new Vector3(horizontalInput, 0f, verticalInput) * moveSpeed * Time.deltaTime;
        transform.Translate(moveDirection, Space.Self);
    }

    private void Jump()
    {
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }
    }

    private void OnCollisionStay(Collision collision)
    {
        isGrounded = true;
    }

    private void OnCollisionExit(Collision collision)
    {
        isGrounded = false;
    }

    private void LookAround()
    {
        float mouseX = Input.GetAxis("Mouse X") * lookSpeed * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * lookSpeed * Time.deltaTime;
        verticalRotation -= mouseY; // Subtract to get the correct rotation
        verticalRotation = Mathf.Clamp(verticalRotation, -90f, 90f);
        transform.Rotate(Vector3.up * mouseX);
        Camera.main.transform.localRotation = Quaternion.Euler(verticalRotation, 0f, 0f);
    }

    private void LockUnlockCursor()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            isCursorLocked = !isCursorLocked;
            Cursor.lockState = isCursorLocked ? CursorLockMode.Locked : CursorLockMode.None;
            Cursor.visible = !isCursorLocked;
        }
    }

    private void ApplyFallDamage()
{
    float fallDistance = lastPosition.y - transform.position.y;
    if (rb.velocity.y < 0 && fallDistance >= fallDamageThreshold)
    {
        TakeDamage(fallDamageAmount);
    }
}


    private void TakeDamage(int amount)
    {
        currentHealth -= amount;
        Debug.Log("Player took " + amount + " damage!");
        if (currentHealth <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        Debug.Log("Player died!");
        // Implement your death logic here, such as resetting the player's position or restarting the level
    }
}
