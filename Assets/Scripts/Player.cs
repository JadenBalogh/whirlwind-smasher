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
    [SerializeField] private LayerMask groundedMask;
    [SerializeField] private float groundedDist = 0.4f;

    [Header("Energy")]
    [SerializeField] private float maxEnergy = 100f;
    public float MaxEnergy { get { return maxEnergy; } }
    [SerializeField] private float startEnergy = 100f;
    [SerializeField] private float dragEnergyDrainRate = 5f;
    [SerializeField] private Color hitBlinkColor = Color.white;
    [SerializeField] private float hitBlinkTime = 0.3f;

    [Header("Combat")]
    [SerializeField] private float impactDamage = 10f;
    [SerializeField] private float damagePerSecond = 10f;
    [SerializeField] private float impactForceTransferRatio = 0.5f;
    [SerializeField] private float knockbackForcePerSecond = 5f;
    [SerializeField] private AudioClip hurtSound;

    private float energy;
    public float Energy { get { return energy; } }

    private bool isAttacking;
    public bool IsAttacking { get { return isAttacking; } }

    private bool alive = true;
    private float dragTimer = 0f;
    private Vector2 groundedArea;
    private Vector3 groundedOffset;

    private Coroutine hitBlinkCoroutine;
    private YieldInstruction hitBlinkInstruction;

    private new Rigidbody2D rigidbody2D;
    private Animator animator;
    private SpriteRenderer spriteRenderer;
    private AudioSource audioSource;

    private EnergyChangedEvent onEnergyChanged = new EnergyChangedEvent();

    void Awake()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        audioSource = GetComponent<AudioSource>();

        hitBlinkInstruction = new WaitForSeconds(hitBlinkTime);

        groundedArea = new Vector2(spriteRenderer.bounds.size.x, groundedDist);
        groundedOffset = Vector2.down * (spriteRenderer.bounds.extents.y + groundedDist / 2);

        energy = startEnergy;
        alive = true;
    }

    void Update()
    {
        Collider2D hit = Physics2D.OverlapBox(transform.position + groundedOffset, groundedArea, groundedDist, groundedMask);
        animator.SetBool("IsGrounded", hit != null);
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
        bool isRotating = animator.GetBool("IsRotating");
        transform.rotation = isRotating ? Quaternion.FromToRotation(Vector2.right, dragVector) : Quaternion.identity;
        spriteRenderer.flipX = !isRotating && dragVector.x < 0;

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
        if (isAttacking && col.TryGetComponent<Enemy>(out Enemy enemy))
        {
            enemy.StartSmear();
            enemy.Splatter((transform.position - enemy.transform.position).normalized);
            enemy.Knockback(rigidbody2D.velocity * impactForceTransferRatio, ForceMode2D.Impulse);
            AddEnergy(enemy.TakeDamage(impactDamage));
        }

        if (col.TryGetComponent<Barrel>(out Barrel barrel))
        {
            barrel.Break();
        }
    }

    void OnTriggerStay2D(Collider2D col)
    {
        if (isAttacking && col.TryGetComponent<Enemy>(out Enemy enemy))
        {
            enemy.Knockback(rigidbody2D.velocity.normalized * knockbackForcePerSecond);
            AddEnergy(enemy.TakeDamage(damagePerSecond * Time.deltaTime));
        }
    }

    void OnTriggerExit2D(Collider2D col)
    {
        if (col.TryGetComponent<Enemy>(out Enemy enemy))
        {
            enemy.StopSmear();
        }
    }

    public void AddEnergy(float increase)
    {
        energy = Mathf.Min(maxEnergy, energy + increase);
        onEnergyChanged.Invoke(energy, maxEnergy);
    }

    public void ReduceEnergy(float reduction, bool blink = false)
    {
        energy = Mathf.Max(0, energy - reduction);
        onEnergyChanged.Invoke(energy, maxEnergy);

        if (blink)
        {
            audioSource.PlayOneShot(hurtSound);

            if (hitBlinkCoroutine != null) StopCoroutine(hitBlinkCoroutine);
            hitBlinkCoroutine = StartCoroutine(HitBlink());
        }

        if (energy <= 0)
        {
            alive = false;
            HUD.Instance.Hide();
            DeathMenu.Instance.Reveal();
            spriteRenderer.enabled = false;
        }
    }

    private Vector3 GetMousePosition()
    {
        return Camera.main.ScreenToWorldPoint(Input.mousePosition);
    }

    private IEnumerator HitBlink()
    {
        spriteRenderer.color = hitBlinkColor;
        yield return hitBlinkInstruction;
        spriteRenderer.color = Color.white;
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
