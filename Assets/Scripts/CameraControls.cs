using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControls : MonoBehaviour
{
    private Player player;
    [SerializeField] private float followThreshold = 0.5f;
    [SerializeField] private float smoothTime = 0.3f;
    private Vector3 velocity = Vector3.zero;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
    }

    void Update()
    {
        float playerXPositionViewPort = Camera.main.WorldToViewportPoint(player.transform.position).x;
        if (playerXPositionViewPort >= followThreshold)
        {
            Vector3 goalPosition = new Vector3(player.transform.position.x, transform.position.y, transform.position.z);
            transform.position = Vector3.SmoothDamp(transform.position, goalPosition, ref velocity, smoothTime);
        }
    }
}
