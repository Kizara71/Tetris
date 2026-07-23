using System;
using UnityEngine;

namespace Tetris
{
    public class ScoreManager : MonoBehaviour
    {
        public int score { get; private set; } = 0;
        public int level { get; private set; } = 1;
        public int totalLinesCleared { get; private set; } = 0;
        public int multiplier { get; private set; } = 1;
        public bool isFrenzy { get; private set; } = false;
        
        // Event to notify the UI that the stats have updated
        public event Action OnScoreUpdated;
        public event Action<int> OnLevelChanged;
    
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
            totalLinesCleared += linesCleared;
            
            int oldLevel = level;
            // Calculate Level (Level 1 is 0-3 lines, Level 2 is 4-7, etc.)
            level = Mathf.FloorToInt(totalLinesCleared / 4f) + 1;
            
            if (level > oldLevel)
            {
                OnLevelChanged?.Invoke(level);
            }
            
            // Calculate Multiplier
            int baseMultiplier = level;
            isFrenzy = (level % 2 == 0);
            multiplier = baseMultiplier * (isFrenzy ? 2 : 1);
    
            int basePoints = 0;
    
            switch (linesCleared)
            {
                case 1: basePoints = 100; break;
                case 2: basePoints = 300; break;
                case 3: basePoints = 500; break;
                case 4: basePoints = 800; break;
                default: if (linesCleared > 4) basePoints = 1000; break;
            }
    
            int pointsEarned = basePoints * multiplier;
            AddScore(pointsEarned);
        }
    
        public void AddScore(int points)
        {
            score += points;
            OnScoreUpdated?.Invoke();
        }
    
        public void ResetScore()
        {
            score = 0;
            level = 1;
            totalLinesCleared = 0;
            multiplier = 1;
            isFrenzy = false;
            OnScoreUpdated?.Invoke();
        }
    }
    
}

