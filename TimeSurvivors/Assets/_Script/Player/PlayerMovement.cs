using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Movement")]
    private Vector2 moveInput;
    private Rigidbody2D rb;

    [Header("Dash")]
    public float dashForce = 12f;
    public float dashDuration = 0.2f;
    public float dashCooldown = 1f;
    private bool isDashing = false;
    private float dashCooldownTimer = 0f;
    private Vector2 dashDirection;

    [Header("Invincibility")]
    public bool isInvincible = false;
    private Collider2D playerCollider;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        playerCollider = GetComponent<Collider2D>();
    }

    void Update()
    {
        if (!isDashing)
        {
            moveInput = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")).normalized;
        }

        if (Input.GetKeyDown(KeyCode.Space) && dashCooldownTimer <= 0f && moveInput != Vector2.zero)
        {
            StartDash();
        }

        if (dashCooldownTimer > 0f)
            dashCooldownTimer -= Time.deltaTime;
    }

    void FixedUpdate()
    {
        if (isDashing)
        {
            rb.velocity = dashDirection * dashForce;
        }
        else
        {
            rb.velocity = moveInput * PlayerStats.instance.moveSpeed;
        }
    }

    void StartDash()
    {
        isDashing = true;
        isInvincible = true;
        dashDirection = moveInput;
        dashCooldownTimer = dashCooldown;
        playerCollider.enabled = false;

        Invoke(nameof(EndDash), dashDuration);
    }

    void EndDash()
    {
        isDashing = false;
        isInvincible = false;

        playerCollider.enabled = true;
    }
}
