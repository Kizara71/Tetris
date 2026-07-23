using UnityEngine;

namespace Tetris
{
    public class GameOverState : IGameState
    {
        private Board board;
    
        public GameOverState(Board board)
        {
            this.board = board;
        }
    
        public void EnterState()
        {
            // Tell UI it's game over
            if (board != null)
            {
                board.tilemap.ClearAllTiles();
            }
        }
    
        public void UpdateState()
        {
            // Do nothing, wait for restart
        }
    
        public void ExitState()
        {
            // Cleanup game over screen
        }
    }
    
}

