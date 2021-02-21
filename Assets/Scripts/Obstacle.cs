using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
    void Awake()
    {
        gameObject.AddComponent(typeof(DestroyOnLeaveScreen));
    }
}
