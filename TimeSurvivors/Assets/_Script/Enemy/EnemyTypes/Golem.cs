using UnityEngine;

public class Golem : MonoBehaviour
{
    public float speed = 1f;
    public float slamRange = 1.5f;
    public float slamRadius = 2f;
    public float slamChargeTime = 1.2f;
    public float slamCooldown = 2f;
    public int slamDamage = 30;

    public Transform player;
    public LayerMask playerLayer;
    public GameObject slamIndicatorPrefab; 

    private float slamTimer = 0f;
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
            Vector2 dir = (player.position - transform.position).normalized;
            transform.position += (Vector3)dir * speed * Time.deltaTime;
        }
    }

    System.Collections.IEnumerator DoSlam()
    {
        isSlamming = true;

        // Show AoE Indicator
        currentIndicator = Instantiate(slamIndicatorPrefab, transform.position, Quaternion.identity);
        //currentIndicator.transform.localScale = Vector3.one * slamRadius * 2f;

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

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, slamRadius);
    }
}
