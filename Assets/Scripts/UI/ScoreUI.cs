using UnityEngine;
using TMPro;

namespace Tetris
{
    public class ScoreUI : MonoBehaviour
    {
        public ScoreManager scoreManager;
        public TextMeshProUGUI scoreText;
        public TextMeshProUGUI levelText;
        public TextMeshProUGUI multiplierText;
    
        private void OnEnable()
        {
            if (scoreManager != null)
            {
                scoreManager.OnScoreUpdated += UpdateUI;
            }
        }
    
        private void OnDisable()
        {
            if (scoreManager != null)
            {
                scoreManager.OnScoreUpdated -= UpdateUI;
            }
        }
    
        private void Start()
        {
            // Initialize the UI with the current score
            if (scoreManager != null)
            {
                UpdateUI();
            }
        }
    
        private void UpdateUI()
        {
            if (scoreText != null)
            {
                scoreText.text = "Score: " + scoreManager.score.ToString(); 
            }
    
            if (levelText != null)
            {
                levelText.text = "Level: " + scoreManager.level.ToString();
            }
    
            if (multiplierText != null)
            {
                string frenzyText = scoreManager.isFrenzy ? " (FRENZY!)" : "";
                multiplierText.text = "Multiplier: x" + scoreManager.multiplier.ToString() + frenzyText;
            }
        }
    }
    
}

