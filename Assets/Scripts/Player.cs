using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Player : MonoBehaviour
{
    public class EnergyChangedEvent : UnityEvent<float, float> { }

    [Header("Controls")]
    [SerializeField] private float maxDragTime = 0.5f;
    [SerializeField] private float dragAccelTime = 0.3f;
    [SerializeField] private float maxDragSpeed = 20f;

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

    private bool alive = true;
    private Vector3 startPos;
    private Vector3 prevMousePos;
    private float dragTimer = 0f;

    private new Rigidbody2D rigidbody2D;

    private EnergyChangedEvent onEnergyChanged = new EnergyChangedEvent();

    void Awake()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
        energy = startEnergy;
        alive = true;
    }

    void OnMouseDown()
    {
        startPos = transform.position;
        prevMousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        dragTimer = 0;
    }

    void OnMouseDrag()
    {
        if (!alive) return;
        if (dragTimer > maxDragTime) return;
        if (!IsMouseMoved()) return;

        dragTimer += Time.deltaTime;

        float dragSpeed = Mathf.Lerp(0, maxDragSpeed, dragTimer / dragAccelTime);
        rigidbody2D.velocity = dragSpeed * GetDragDirection();

        ReduceEnergy(dragEnergyDrainRate * Time.deltaTime);

        prevMousePos = transform.position;
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.TryGetComponent<Enemy>(out Enemy enemy))
        {
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

    ///<summary>Returns the direction from the player's start position to the mouse position.</summary>
    private Vector2 GetDragDirection()
    {
        Vector2 offset = Camera.main.ScreenToWorldPoint(Input.mousePosition) - startPos;
        return offset.normalized;
    }

    ///<summary>Returns the direction from the player's start position to the mouse position.</summary>
    private bool IsMouseMoved()
    {
        Vector3 currentMousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        return currentMousePos != prevMousePos;
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
