using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenu : CanvasMenu
{
    private static MainMenu instance;
    public static MainMenu Instance { get { return instance; } }

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
