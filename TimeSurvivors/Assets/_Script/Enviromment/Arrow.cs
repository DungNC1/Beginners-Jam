using UnityEngine;

public class Arrow : MonoBehaviour
{
    public int damage = 10;
    public float lifetime = 5f;
    public LayerMask hitLayer;

    void Start()
    {
        Destroy(gameObject, lifetime);
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
