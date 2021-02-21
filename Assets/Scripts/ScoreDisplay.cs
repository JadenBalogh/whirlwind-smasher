using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreDisplay : MonoBehaviour
{
    [SerializeField] private string prefix = "";

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
        textBox.text = prefix + score.ToString("F1") + " m";
    }
}
