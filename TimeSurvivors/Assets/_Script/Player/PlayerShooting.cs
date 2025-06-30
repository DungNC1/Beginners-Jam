using UnityEngine;

public class PlayerShooting : MonoBehaviour
{
    [Header("Shooting")]
    public GameObject projectilePrefab; 
    public float projectileSpeed = 10f;
    public float range = 10f;

    private float fireCooldown = 0f;

    void Update()
    {
        fireCooldown -= Time.deltaTime;

        if (fireCooldown <= 0f)
        {
            Transform target = FindNearestEnemy();

            if (target != null)
            {
                ShootAt(target);
                fireCooldown = PlayerStats.instance.fireRate;
            }
        }
    }

    Transform FindNearestEnemy()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        Transform nearest = null;
        float shortestDist = Mathf.Infinity;

        foreach (GameObject enemy in enemies)
        {
            float dist = Vector2.Distance(transform.position, enemy.transform.position);
            if (dist < shortestDist && dist <= range)
            {
                shortestDist = dist;
                nearest = enemy.transform;
            }
        }

        return nearest;
    }

    void ShootAt(Transform target)
    {
        Vector2 direction = (target.position - transform.position).normalized;
        GameObject bullet = Instantiate(projectilePrefab, transform.position, Quaternion.identity);
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();

        if (rb != null)
            rb.velocity = direction * projectileSpeed;

        // Optional: Rotate projectile to face target
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        bullet.transform.rotation = Quaternion.Euler(0, 0, angle);
    }
}
