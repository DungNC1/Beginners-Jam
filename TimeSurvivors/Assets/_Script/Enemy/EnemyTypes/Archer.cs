using UnityEngine;

public class Archer : MonoBehaviour
{
    public float speed = 2f;
    public float fireRange = 8f;
    public float fireCooldown = 2.5f;
    public GameObject arrowPrefab;
    public Transform firePoint;
    public Transform player;

    private float cooldownTimer = 0f;

    void Update()
    {
        if (!player) return;

        float dist = Vector2.Distance(transform.position, player.position);
        cooldownTimer -= Time.deltaTime;

        if (dist > fireRange)
        {
            Vector2 dir = (player.position - transform.position).normalized;
            transform.position += (Vector3)dir * speed * Time.deltaTime;
        }
        else
        {
            if (cooldownTimer <= 0f)
            {
                FireArrow();
                cooldownTimer = fireCooldown;
            }
        }
    }

    void FireArrow()
    {
        Vector2 dir = (player.position - firePoint.position).normalized;
        GameObject arrow = Instantiate(arrowPrefab, firePoint.position, Quaternion.identity);
        arrow.GetComponent<Rigidbody2D>().velocity = dir * 6f;
    }
}
