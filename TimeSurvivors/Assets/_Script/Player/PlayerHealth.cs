using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public static PlayerHealth instance;
    public int maxHealth;
    private int currentHealth;

    private void Awake()
    {

        instance = this;
        currentHealth = maxHealth;
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        if(currentHealth <= 0)
        {
            //...
        }
    }
}
