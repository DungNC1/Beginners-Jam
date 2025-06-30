using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Enemy") && collision.tag != gameObject.tag)
        {
            collision.gameObject.GetComponent<IDamageable>().TakeDamage((int)PlayerStats.instance.damage);
            Destroy(gameObject);
        }

    }
}
