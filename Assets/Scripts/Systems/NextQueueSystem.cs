using System.Collections.Generic;
using UnityEngine;

namespace Tetris
{
    public class NextQueueSystem : MonoBehaviour
    {
        private Queue<TetrominoData> nextPieces = new Queue<TetrominoData>();
        private IPieceFactory pieceFactory;
        
        // Array of displays to support multiple next pieces (like Tetris 99 or modern Tetris)
        public PieceDisplay[] displays;
    
        private void Awake()
        {
            pieceFactory = GetComponent<IPieceFactory>();
        }
    
        public void InitializeQueue()
        {
            nextPieces.Clear();
            int queueSize = Mathf.Max(1, displays != null ? displays.Length : 1);
            
            for (int i = 0; i < queueSize; i++)
            {
                nextPieces.Enqueue(pieceFactory.CreatePiece());
            }
            UpdateDisplay();
        }
    
        public TetrominoData GetNextPiece()
        {
            TetrominoData next = nextPieces.Dequeue();
            nextPieces.Enqueue(pieceFactory.CreatePiece());
            UpdateDisplay();
            return next;
        }
    
        private void UpdateDisplay()
        {
            if (displays == null || displays.Length == 0) return;
    
            TetrominoData[] queueArray = nextPieces.ToArray();
            
            for (int i = 0; i < displays.Length; i++)
            {
                if (displays[i] != null && i < queueArray.Length)
                {
                    displays[i].DrawPiece(queueArray[i]);
                }
            }
        }
    }
    
}

