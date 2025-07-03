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

    [Header("Animation")]
    public Animator animator;
    public string idleAnim = "Idle";
    public string walkAnim = "Walk";

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

            // Handle animation
            if (moveInput != Vector2.zero)
            {
                animator.Play(walkAnim);

                // Flip player based on horizontal direction
                if (moveInput.x != 0)
                {
                    transform.localScale = new Vector3(Mathf.Sign(moveInput.x), 1, 1);
                }
            }
            else
            {
                animator.Play(idleAnim);
            }
        }

        if (Input.GetKeyDown(KeyCode.Space) && dashCooldownTimer <= 0f && moveInput != Vector2.zero)
        {
            StartDash();
        }

        if (dashCooldownTimer > 0f)
        {
            dashCooldownTimer -= Time.deltaTime;
        }
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
