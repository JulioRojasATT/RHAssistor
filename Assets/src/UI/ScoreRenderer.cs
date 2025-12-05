using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using System;

public class ScoreRenderer : MonoBehaviour
{
    [SerializeField] private IntScriptableValue scoreValue;

    [SerializeField] private TextMeshProUGUI scoreText;

    private EventHandler<ScriptableValueChangedEvent<int>> onScoreChangedEvent;

    private void Awake()
    {
        onScoreChangedEvent += OnScoreChanged;
        scoreValue.OnValueChangedEvent += onScoreChangedEvent;
    }

    public void OnScoreChanged(object invoker, ScriptableValueChangedEvent<int> intValueChangedEventArgs)
    {
        DisplayScore(intValueChangedEventArgs.newValue);
    }

    public void DisplayScore(int score)
    {
        if (scoreText != null)
        {
            scoreText.text = "" + scoreValue.Value;
        }
    }

    public void DisplayScore(IntScriptableValue scoreValue)
    {
        DisplayScore(scoreValue.Value);
    }
}