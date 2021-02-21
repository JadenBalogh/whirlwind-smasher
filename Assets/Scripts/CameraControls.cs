using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControls : MonoBehaviour
{
    [SerializeField] private float followThreshold = 0.5f;
    [SerializeField] private float smoothTime = 0.3f;
    [SerializeField] private Transform wall;

    private Vector3 velocity = Vector3.zero;
    private Player player;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        UpdateWall();
    }

    void FixedUpdate()
    {
        float playerViewportX = Camera.main.WorldToViewportPoint(player.transform.position).x;
        if (playerViewportX > followThreshold)
        {
            // Move camera forward to follow the player
            float screenLeft = Camera.main.ViewportToWorldPoint(Vector3.zero).x;
            float cameraViewportOffsetX = screenLeft - Camera.main.ViewportToWorldPoint(Vector3.right * (followThreshold - 0.5f)).x;
            Vector3 cameraTargetPosition = new Vector3(player.transform.position.x - cameraViewportOffsetX, transform.position.y, transform.position.z);
            transform.position = Vector3.SmoothDamp(transform.position, cameraTargetPosition, ref velocity, smoothTime);

            // Move the wall forward 
            UpdateWall();
        }
    }

    private void UpdateWall()
    {
        wall.position = new Vector3(Camera.main.ViewportToWorldPoint(Vector3.zero).x - 0.5f, wall.position.y, wall.position.z);
    }
}
