using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    public static PlayerStats instance; 

    private void Awake()
    {
        instance = this;
    }

    public float moveSpeed = 3;
    public float fireRate = 1.9f;
    public int damage = 15;
    public float critChance = 5f;
    public float maxHealth = 100f;
    public float health = 100f;
}
