using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreDisplay : MonoBehaviour
{
    private Text textBox;

    void Awake()
    {
        textBox = GetComponent<Text>();
    }

    void Start()
    {
        GameManager.ScoreSystem.AddScoreChangedListener(UpdateScore);
    }

    private void UpdateScore(float score)
    {
        textBox.text = score.ToString("F1") + " m";
    }
}
