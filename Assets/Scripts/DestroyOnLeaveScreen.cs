using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyOnLeaveScreen : MonoBehaviour
{
    void Update()
    {
        if (transform.position.x < Camera.main.ScreenToWorldPoint(Vector2.zero).x - transform.localScale.x) Destroy(gameObject);
    }
}
