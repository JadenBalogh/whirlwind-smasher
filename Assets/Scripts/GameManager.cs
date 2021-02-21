using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static GameManager instance;

    private static ScoreSystem scoreSystem;
    public static ScoreSystem ScoreSystem { get { return scoreSystem; } }
    private static EnvironmentBuilder environmentBuilder;

    void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
            return;
        }
        instance = this;

        scoreSystem = GetComponent<ScoreSystem>();
        environmentBuilder = GetComponent<EnvironmentBuilder>();
    }
}
