using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControls : MonoBehaviour
{
    private Player player;
    GameObject wall;
    [SerializeField] private float followThreshold = 0.5f;
    [SerializeField] private float smoothTime = 0.3f;
    private Vector3 velocity = Vector3.zero;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        wall = GameObject.FindGameObjectWithTag("Wall");
    }

    void Update()
    {
        float playerXPositionViewPort = Camera.main.WorldToViewportPoint(player.transform.position).x;
        if (playerXPositionViewPort >= followThreshold)
        {
            // Move camera forward to follow the player
            Vector3 cameraTargetPosition = new Vector3(player.transform.position.x, transform.position.y, transform.position.z);
            transform.position = Vector3.SmoothDamp(transform.position, cameraTargetPosition, ref velocity, smoothTime);

            // Move the wall forward 
            // TODO: set the below numbers to be dynamically-obtained values (or have constants set more formally)
            Vector3 wallTargetPosition = new Vector3(transform.position.x - 8.4f, 4, 10);
            wall.transform.position = wallTargetPosition;
        }
    }
}
