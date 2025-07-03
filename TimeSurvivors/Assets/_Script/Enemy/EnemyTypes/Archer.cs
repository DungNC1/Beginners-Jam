using UnityEngine;

public class Archer : MonoBehaviour
{
    [Header("Movement & Attack")]
    public float speed = 2f;
    public float fireRange = 8f;
    public float fireCooldown = 2.5f;
    public GameObject arrowPrefab;
    public Transform firePoint;

    [Header("Animation")]
    public Animator animator;
    public string walkAnim = "Walk";
    public string shootAnim = "Shoot";

    private Transform player;
    private float cooldownTimer = 0f;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player")?.transform;
    }

    private void Update()
    {
        if (!player) return;

        float dist = Vector2.Distance(transform.position, player.position);
        cooldownTimer -= Time.deltaTime;

        if (dist > fireRange)
        {
            // Move toward player
            Vector2 dir = (player.position - transform.position).normalized;
            transform.position += (Vector3)dir * speed * Time.deltaTime;

            // Face player
            if (dir.x != 0)
                transform.localScale = new Vector3(Mathf.Sign(dir.x), 1, 1);

            // Play walk animation
            if (animator) animator.Play(walkAnim);
        }
        else
        {
            // Stop moving and fire if ready
            if (cooldownTimer <= 0f)
            {
                if (animator) animator.Play(shootAnim);
                cooldownTimer = fireCooldown;
            }
        }
    }

    public void FireArrow()
    {
        if (!arrowPrefab || !firePoint || !player) return;

        Vector2 dir = (player.position - firePoint.position).normalized;
        GameObject arrow = Instantiate(arrowPrefab, firePoint.position, Quaternion.identity);
    }
}
