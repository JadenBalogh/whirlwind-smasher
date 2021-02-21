using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Smear : MonoBehaviour
{
    private Transform target;

    public void Attach(Transform target)
    {
        this.target = target;
    }

    public void Detach()
    {
        target = null;
    }

    void Update()
    {
        if (target != null)
        {
            transform.position = target.position;
        }
    }
}
