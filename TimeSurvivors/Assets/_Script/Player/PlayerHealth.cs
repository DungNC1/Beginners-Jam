using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public static PlayerHealth instance;

    public void TakeDamage(int damage)
    {
        PlayerStats.instance.health -= damage;
        if (PlayerStats.instance.health <= 0)
        {
            //...
        }
    }
}
