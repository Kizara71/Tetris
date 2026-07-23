using UnityEngine;
using UnityEngine.SceneManagement;

namespace Tetris
{
    public class GameOverUI : MonoBehaviour
    {
        [Header("UI References")]
        public GameObject gameOverPanel; // The panel containing your Game Over text and Restart Button
    
        private void Start()
        {
            // Hide the game over panel when the game starts
            if (gameOverPanel != null)
            {
                gameOverPanel.SetActive(false);
            }
        }
    
        private void OnEnable()
        {
            if (EventManager.Instance != null)
            {
                EventManager.Instance.OnGameOver += ShowGameOverScreen;
            }
        }
    
        private void OnDisable()
        {
            if (EventManager.Instance != null)
            {
                EventManager.Instance.OnGameOver -= ShowGameOverScreen;
            }
        }
    
        private void ShowGameOverScreen()
        {
            if (gameOverPanel != null)
            {
                gameOverPanel.SetActive(true);
            }
        }
    
        // Call this method from your UI Button's OnClick event
        public void RestartGame()
        {
            // Reloads the current active scene
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }
    
}

