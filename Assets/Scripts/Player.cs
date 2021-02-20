using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private float maxDragTime = 0.5f;
    [SerializeField] private float dragAccelTime = 0.3f;
    [SerializeField] private float maxDragSpeed = 20f;

    private Vector3 startPos;
    private float dragTimer = 0f;

    private new Rigidbody2D rigidbody2D;

    void Awake()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
    }

    void OnMouseDown()
    {
        startPos = transform.position;
        dragTimer = 0;
    }

    void OnMouseDrag()
    {
        if (dragTimer > maxDragTime) return;

        dragTimer += Time.deltaTime;
        float dragSpeed = Mathf.Lerp(0, maxDragSpeed, dragTimer / dragAccelTime);
        rigidbody2D.velocity = dragSpeed * GetDragDirection();
    }

    ///<summary>Returns the direction from the player's start position to the mouse position.</summary>
    private Vector2 GetDragDirection()
    {
        Vector2 offset = Camera.main.ScreenToWorldPoint(Input.mousePosition) - startPos;
        return offset.normalized;
    }
}
