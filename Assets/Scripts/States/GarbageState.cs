using UnityEngine;

namespace Tetris
{
    public class GarbageState : IGameState
    {
        private Board board;
        private float garbageTimer = 10f;
        private int linesToSpawn = 4;
        private bool hasSpawnedGarbage = false;
    
        public GarbageState(Board board)
        {
            this.board = board;
        }
    
        public void EnterState()
        {
            garbageTimer = 10f;
            hasSpawnedGarbage = false;
            // Could trigger UI warning here
        }
    
        public void UpdateState()
        {
            if (board == null) return;
    
            // Still allow normal gameplay
            if (board.activePiece != null)
            {
                board.activePiece.UpdatePiece();
            }
    
            // Timer logic
            if (!hasSpawnedGarbage)
            {
                garbageTimer -= Time.deltaTime;
                
                if (garbageTimer <= 0)
                {
                    board.AddGarbageLines(linesToSpawn);
                    hasSpawnedGarbage = true;
                    
                    // Return to normal playing state
                    GameStateManager.Instance.ChangeState(new PlayingState(board));
                }
            }
        }
    
        public void ExitState()
        {
            // Clean up UI warning
        }
    }
    
}

