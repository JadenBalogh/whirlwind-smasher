using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private float maxHealth = 10f;

    private float health;

    private new Rigidbody2D rigidbody2D;

    void Awake()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
        health = maxHealth;
    }

    public void TakeDamage(float damage)
    {
        health -= damage;
        if (health <= 0) Die();
    }

    public void Knockback(Vector2 force, ForceMode2D mode = ForceMode2D.Force)
    {
        rigidbody2D.AddForce(force, mode);
    }

    private void Die()
    {
        Destroy(gameObject);
    }
}
