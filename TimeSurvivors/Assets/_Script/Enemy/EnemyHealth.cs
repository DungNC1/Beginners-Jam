using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour, IDamageable
{
    public int health;
    private int currentHealth;
    public int expAmount;
    public GameObject exp;

    public void TakeDamage()
    {
        currentHealth--;

        if(currentHealth <= 0)
        {
            for (int i = 0; i < expAmount; i++)
            {
                Instantiate(exp);
            }
            Destroy(gameObject);
        }
    }
}
