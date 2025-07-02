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
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            other.gameObject.GetComponent<PlayerHealth>()?.TakeDamage(3);
        }
    }
}
