using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Barrel : MonoBehaviour
{
    [SerializeField] private GameObject breakPrefab;
    [SerializeField] private GameObject explosionPrefab;
    [SerializeField] private float energyHitDrain = 10f;
    [SerializeField] private AudioClip breakSound;

    private Animator animator;
    private AudioSource audioSource;

    void Awake()
    {
        animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.TryGetComponent<Player>(out Player player))
        {
            if (player.IsAttacking)
            {
                player.AddEnergy(energyHitDrain);
            }
            else
            {
                player.ReduceEnergy(energyHitDrain, true);
            }
        }
    }

    public void Break()
    {
        animator.SetTrigger("Explode");
        Instantiate(breakPrefab, transform.position, Quaternion.identity);
        Instantiate(explosionPrefab, transform.position, Quaternion.identity);
        audioSource.PlayOneShot(breakSound);
        Destroy(gameObject, 1f);
    }
}
