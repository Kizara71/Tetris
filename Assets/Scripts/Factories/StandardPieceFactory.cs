using UnityEngine;

namespace Tetris
{
    public class StandardPieceFactory : MonoBehaviour, IPieceFactory
    {
        [SerializeField] private TetrominoData[] tetrominoes;
    
        private void Awake()
        {
            // Initialize the cells for all tetrominoes when the game starts
            for (int i = 0; i < tetrominoes.Length; i++)
            {
                tetrominoes[i].InitializeData();
            }
        }
    
        public TetrominoData CreatePiece()
        {
            int random = Random.Range(0, tetrominoes.Length);
            return tetrominoes[random];
        }
    }
    
}

