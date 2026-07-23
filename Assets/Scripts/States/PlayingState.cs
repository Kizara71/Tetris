using UnityEngine;

namespace Tetris
{
    public class PlayingState : IGameState
    {
        private Board board;
    
        public PlayingState(Board board)
        {
            this.board = board;
        }
    
        public void EnterState()
        {
            // Resume game if paused, etc.
        }
    
        public void UpdateState()
        {
            if (board != null && board.activePiece != null)
            {
                board.activePiece.UpdatePiece();
            }
        }
    
        public void ExitState()
        {
            // Clean up
        }
    }
    
}

