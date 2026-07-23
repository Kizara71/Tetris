using System;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    public int score { get; private set; } = 0;
    
    // Event to notify the UI that the score has updated
    public event Action<int> OnScoreChanged;

    private void OnEnable()
    {
        if (EventManager.Instance != null)
        {
            EventManager.Instance.OnLinesCleared += HandleLinesCleared;
        }
    }

    private void OnDisable()
    {
        if (EventManager.Instance != null)
        {
            EventManager.Instance.OnLinesCleared -= HandleLinesCleared;
        }
    }

    private void HandleLinesCleared(int linesCleared)
    {
        int pointsEarned = 0;

        switch (linesCleared)
        {
            case 1:
                pointsEarned = 100;
                break;
            case 2:
                pointsEarned = 300;
                break;
            case 3:
                pointsEarned = 500;
                break;
            case 4:
                pointsEarned = 800; // Tetris!
                break;
            default:
                if (linesCleared > 4)
                {
                    pointsEarned = 1000;
                }
                break;
        }

        AddScore(pointsEarned);
    }

    public void AddScore(int points)
    {
        score += points;
        OnScoreChanged?.Invoke(score);
    }

    public void ResetScore()
    {
        score = 0;
        OnScoreChanged?.Invoke(score);
    }
}
