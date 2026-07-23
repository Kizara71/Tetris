using UnityEngine;
using TMPro;

namespace Tetris
{
    public class GameStateManager : Singleton<GameStateManager>
    {
        public IGameState CurrentState { get; private set; }
    
        [Header("Debug")]
        public TextMeshProUGUI debugStateText;
    
        private Board board;
    
        private void Start()
        {
            board = FindFirstObjectByType<Board>();
            ChangeState(new PlayingState(board));
        }
    
        private void OnEnable()
        {
            if (EventManager.Instance != null)
            {
                EventManager.Instance.OnGameOver += HandleGameOver;
            }
    
            ScoreManager scoreManager = FindFirstObjectByType<ScoreManager>();
            if (scoreManager != null)
            {
                scoreManager.OnLevelChanged += HandleLevelChanged;
            }
        }
    
        private void OnDisable()
        {
            if (EventManager.Instance != null)
            {
                EventManager.Instance.OnGameOver -= HandleGameOver;
            }
    
            ScoreManager scoreManager = FindFirstObjectByType<ScoreManager>();
            if (scoreManager != null)
            {
                scoreManager.OnLevelChanged -= HandleLevelChanged;
            }
        }
    
        private void HandleGameOver()
        {
            ChangeState(new GameOverState(board));
        }
    
        private void HandleLevelChanged(int level)
        {
            if (level % 3 == 0)
            {
                ChangeState(new GarbageState(board));
            }
        }
    
        public void ChangeState(IGameState newState)
        {
            if (CurrentState != null)
            {
                CurrentState.ExitState();
            }
    
            CurrentState = newState;
            CurrentState.EnterState();
    
            if (debugStateText != null)
            {
                debugStateText.text = "State: " + CurrentState.GetType().Name;
            }
        }
    
        private void Update()
        {
            if (CurrentState != null)
            {
                CurrentState.UpdateState();
            }
        }
    }
    
}

