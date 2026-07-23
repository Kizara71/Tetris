using UnityEngine;
using TMPro;

public class ScoreUI : MonoBehaviour
{
    public ScoreManager scoreManager;
    public TextMeshProUGUI scoreText;

    private void OnEnable()
    {
        if (scoreManager != null)
        {
            scoreManager.OnScoreChanged += UpdateScoreText;
        }
    }

    private void OnDisable()
    {
        if (scoreManager != null)
        {
            scoreManager.OnScoreChanged -= UpdateScoreText;
        }
    }

    private void Start()
    {
        // Initialize the UI with the current score
        if (scoreManager != null)
        {
            UpdateScoreText(scoreManager.score);
        }
    }

    private void UpdateScoreText(int newScore)
    {
        if (scoreText != null)
        {
            scoreText.text = "Score: " + newScore.ToString("D6"); // "D6" pads the number with leading zeros, e.g., 000100
        }
    }
}
