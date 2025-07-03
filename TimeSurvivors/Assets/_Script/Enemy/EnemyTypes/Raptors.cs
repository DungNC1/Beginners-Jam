using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Raptor : MonoBehaviour
{
    public float speed = 4f;
    public float zigzagFrequency = 3f;
    public float zigzagAmplitude = 0.5f;

    private Transform player;
    private Rigidbody2D rb;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player")?.transform;
        rb = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        if (player == null) return;

        Vector2 toPlayer = (player.position - transform.position).normalized;
        float zigzagOffset = Mathf.Sin(Time.time * zigzagFrequency) * zigzagAmplitude;
        Vector2 zigzagDir = new Vector2(-toPlayer.y, toPlayer.x) * zigzagOffset;

        Vector2 finalMove = (toPlayer + zigzagDir).normalized * speed;
        rb.velocity = finalMove;

        if (finalMove.x != 0)
        {
            transform.localScale = new Vector3(Mathf.Sign(finalMove.x), 1, 1);
        }
    }



    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            PlayerHealth playerHealth = collision.GetComponent<PlayerHealth>();
            if (playerHealth != null)
            {
                playerHealth.TakeDamage(10);
            }
        }
    }
}
