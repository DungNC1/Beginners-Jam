using UnityEngine;
using UnityEngine.EventSystems;
using static UnityEngine.GraphicsBuffer;

public class Arrow : MonoBehaviour
{
    public int damage = 10;
    public float lifetime = 5f;
    public LayerMask hitLayer;
    private Vector2 moveDirection;
    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        Destroy(gameObject, lifetime);

            moveDirection = ((Vector2)(GameObject.FindWithTag("Player").transform.position - transform.position)).normalized;

            rb.velocity = moveDirection * 6f;

            float angle = Mathf.Atan2(moveDirection.y, moveDirection.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(0, 0, angle);
    }


    void OnTriggerEnter2D(Collider2D col)
    {
        if (((1 << col.gameObject.layer) & hitLayer) != 0)
        {
            if (col.CompareTag("Player"))
            {
                col.GetComponent<PlayerHealth>()?.TakeDamage(damage);
            }

            Destroy(gameObject);
        }
    }
}
