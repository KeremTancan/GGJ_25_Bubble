using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI timerText;
    [SerializeField] private TextMeshProUGUI scoreText;

    private GameManager instance;

    private void Awake()
    {
        instance = GameManager.Instance();
    }

    private void Update()
    {
        timerText.text = "Time: " + instance.gameTimer.ToString("F0");
        scoreText.text = "Score: " + instance.point;
    }
}
