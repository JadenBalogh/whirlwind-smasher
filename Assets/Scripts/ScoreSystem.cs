using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ScoreSystem : MonoBehaviour
{
    public class ScoreChangedEvent : UnityEvent<float> { }

    private float distance = 0f;
    public float Distance { get { return distance; } }

    private float startX;
    private Player player;

    private ScoreChangedEvent onScoreChanged = new ScoreChangedEvent();

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        startX = player.transform.position.x;
    }

    void Update()
    {
        distance = player.transform.position.x - startX;
        onScoreChanged.Invoke(distance);
    }

    public void AddScoreChangedListener(UnityAction<float> listener)
    {
        onScoreChanged.AddListener(listener);
    }

    public void RemoveScoreChangedListener(UnityAction<float> listener)
    {
        onScoreChanged.RemoveListener(listener);
    }
}
