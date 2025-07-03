using UnityEngine;

public class Golem : MonoBehaviour
{
    [Header("Movement and Attack")]
    public float speed = 1f;
    public float slamRange = 1.5f;
    public float slamRadius = 2f;
    public float slamChargeTime = 1.2f;
    public float slamCooldown = 2f;
    public int slamDamage = 30;

    [Header("References")]
    public Transform player;
    public LayerMask playerLayer;
    public GameObject slamIndicatorPrefab;
    public Animator animator;
    public string walkAnim = "Walk";
    public string attackAnim = "Slam";

    private bool isSlamming = false;
    private GameObject currentIndicator;

    void Update()
    {
        if (!player) return;

        float distance = Vector2.Distance(transform.position, player.position);

        if (!isSlamming && distance <= slamRange)
        {
            StartCoroutine(DoSlam());
        }

        if (!isSlamming)
        {
            // Move towards player
            Vector2 dir = (player.position - transform.position).normalized;
            transform.position += (Vector3)dir * speed * Time.deltaTime;

            // Play walking animation
            if (animator) animator.Play(walkAnim);

            // Face the player
            if (dir.x != 0)
            {
                transform.localScale = new Vector3(Mathf.Sign(dir.x), 1, 1);
            }
        }
    }

    System.Collections.IEnumerator DoSlam()
    {
        isSlamming = true;

        // Play attack animation
        if (animator) animator.Play(attackAnim);

        // Show AoE Indicator

        yield return new WaitForSeconds(slamChargeTime);

        Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position, slamRadius, playerLayer);
        foreach (var hit in hits)
        {
            if (hit.CompareTag("Player"))
            {
                hit.GetComponent<PlayerHealth>()?.TakeDamage(slamDamage);
            }
        }

        if (currentIndicator)
            Destroy(currentIndicator);

        yield return new WaitForSeconds(slamCooldown);
        isSlamming = false;
    }

    public void spawnEffect()
    {
        currentIndicator = Instantiate(slamIndicatorPrefab, transform.position, Quaternion.identity);
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, slamRadius);
    }
}
