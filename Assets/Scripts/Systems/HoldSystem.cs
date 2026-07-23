using UnityEngine;

namespace Tetris
{
    public class HoldSystem : MonoBehaviour
    {
        private TetrominoData? heldPiece = null;
        public PieceDisplay display;
        public bool hasHeldThisTurn { get; private set; } = false;
    
        public void ResetHoldLock()
        {
            hasHeldThisTurn = false;
        }
    
        // Returns the piece that was in the hold slot, or null if it was empty
        public TetrominoData? HoldPiece(TetrominoData pieceToHold)
        {
            if (hasHeldThisTurn) return null; // Cannot hold twice in one turn
    
            TetrominoData? previouslyHeld = heldPiece;
            heldPiece = pieceToHold;
            
            hasHeldThisTurn = true;
            UpdateDisplay();
    
            return previouslyHeld;
        }
    
        private void UpdateDisplay()
        {
            if (display != null && heldPiece.HasValue)
            {
                display.DrawPiece(heldPiece.Value);
            }
        }
    }
    
}

