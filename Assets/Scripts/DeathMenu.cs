using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathMenu : CanvasMenu
{
    private static DeathMenu instance;
    public static DeathMenu Instance { get { return instance; } }

    protected override void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
            return;
        }
        instance = this;

        base.Awake();
    }
}
