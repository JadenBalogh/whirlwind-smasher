using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Enemy : MonoBehaviour
{
    [System.Serializable] public class HealthChangedEvent : UnityEvent<float, float> { }

    [SerializeField] private float maxHealth = 10f;
    [SerializeField] private float energyDamage = 5f;
    [SerializeField] private HealthChangedEvent onHealthChanged = new HealthChangedEvent();
    [SerializeField] private Transform spurtSpawn;
    [SerializeField] private GameObject splatterPrefab;
    [SerializeField] private GameObject smearPrefab;
    [SerializeField] private GameObject spurtPrefab;

    private float health;

    private new Rigidbody2D rigidbody2D;

    void Awake()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
        health = maxHealth;
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.TryGetComponent<Player>(out Player player))
        {
            player.ReduceEnergy(energyDamage);
        }
    }

    public void Splatter(Vector2 direction)
    {
        Instantiate(splatterPrefab, transform.position, Quaternion.FromToRotation(Vector2.left, direction));
    }

    public void TakeDamage(float damage)
    {
        health -= damage;
        onHealthChanged.Invoke(health, maxHealth);
        if (health <= 0) Die();
    }

    public void Knockback(Vector2 force, ForceMode2D mode = ForceMode2D.Force)
    {
        rigidbody2D.AddForce(force, mode);
    }

    private void Die()
    {
        Instantiate(spurtPrefab, spurtSpawn.position, Quaternion.identity);
        Destroy(gameObject);
    }

    public void AddHealthChangedListener(UnityAction<float, float> listener)
    {
        onHealthChanged.AddListener(listener);
    }

    public void RemoveHealthChangedListener(UnityAction<float, float> listener)
    {
        onHealthChanged.RemoveListener(listener);
    }
}