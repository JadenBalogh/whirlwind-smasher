using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Player : MonoBehaviour
{
    public class EnergyChangedEvent : UnityEvent<float, float> { }

    [Header("Controls")]
    [SerializeField] private float maxDragTime = 0.5f;
    [SerializeField] private float minDragForce = 1f;
    [SerializeField] private float maxDragForce = 6f;
    [SerializeField] private float maxDragLength = 2.5f;

    [Header("Energy")]
    [SerializeField] private float maxEnergy = 100f;
    public float MaxEnergy { get { return maxEnergy; } }
    [SerializeField] private float startEnergy = 100f;
    [SerializeField] private float dragEnergyDrainRate = 5f;

    [Header("Combat")]
    [SerializeField] private float impactDamage = 10f;
    [SerializeField] private float damagePerSecond = 10f;
    [SerializeField] private float impactForceTransferRatio = 0.5f;
    [SerializeField] private float knockbackForcePerSecond = 5f;

    private float energy;
    public float Energy { get { return energy; } }

    private bool isAttacking;
    public bool IsAttacking { get { return isAttacking; } }

    private bool alive = true;
    private float dragTimer = 0f;

    private new Rigidbody2D rigidbody2D;
    private Animator animator;

    private EnergyChangedEvent onEnergyChanged = new EnergyChangedEvent();

    void Awake()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();

        energy = startEnergy;
        alive = true;
    }

    void OnMouseDown()
    {
        // Death check
        if (!alive) return;

        dragTimer = 0;
        isAttacking = true;
        animator.SetBool("IsAttacking", true);
    }

    void OnMouseDrag()
    {
        // Death check
        if (!alive) return;

        // Limit drag duration
        if (dragTimer > maxDragTime) return;
        dragTimer += Time.deltaTime;

        // Get vector from player to mouse
        Vector2 dragVector = GetMousePosition() - transform.position;

        // Add force based on drag distance
        float dragForce = Mathf.Lerp(minDragForce, maxDragForce, dragVector.magnitude / maxDragLength);
        rigidbody2D.AddForce(dragForce * dragVector.normalized);

        // Rotate to face current direction
        transform.rotation = Quaternion.FromToRotation(Vector2.right, dragVector);

        // Reduce energy over time
        ReduceEnergy(dragEnergyDrainRate * Time.deltaTime);
    }

    void OnMouseUp()
    {
        // Death check
        if (!alive) return;
        
        isAttacking = false;
        animator.SetBool("IsAttacking", false);
        transform.rotation = Quaternion.identity;
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.TryGetComponent<Enemy>(out Enemy enemy))
        {
            enemy.Splatter((transform.position - enemy.transform.position).normalized);
            enemy.TakeDamage(impactDamage);
            enemy.Knockback(rigidbody2D.velocity * impactForceTransferRatio, ForceMode2D.Impulse);
        }
    }

    void OnTriggerStay2D(Collider2D col)
    {
        if (col.TryGetComponent<Enemy>(out Enemy enemy))
        {
            enemy.TakeDamage(damagePerSecond * Time.deltaTime);
            enemy.Knockback(rigidbody2D.velocity.normalized * knockbackForcePerSecond);
        }
    }

    public void AddEnergy(float increase)
    {
        energy = Mathf.Min(maxEnergy, energy + increase);
        onEnergyChanged.Invoke(energy, maxEnergy);
    }

    public void ReduceEnergy(float reduction)
    {
        energy = Mathf.Max(0, energy - reduction);
        onEnergyChanged.Invoke(energy, maxEnergy);
        if (energy <= 0)
        {
            alive = false;
            Debug.Log("Game Over!");
        }
    }

    private Vector3 GetMousePosition()
    {
        return Camera.main.ScreenToWorldPoint(Input.mousePosition);
    }

    public void AddEnergyChangedListener(UnityAction<float, float> listener)
    {
        onEnergyChanged.AddListener(listener);
    }

    public void RemoveEnergyChangedListener(UnityAction<float, float> listener)
    {
        onEnergyChanged.RemoveListener(listener);
    }
}
