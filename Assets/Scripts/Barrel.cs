using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Barrel : MonoBehaviour
{
    [SerializeField] private GameObject breakPrefab;
    [SerializeField] private GameObject explosionPrefab;
    [SerializeField] private float energyHitDrain = 10f;

    private Animator animator;

    void Awake()
    {
        animator = GetComponent<Animator>();
    }

    public float Break()
    {
        animator.SetTrigger("Explode");
        Instantiate(breakPrefab, transform.position, Quaternion.identity);
        Instantiate(explosionPrefab, transform.position, Quaternion.identity);
        return energyHitDrain;
    }
}
