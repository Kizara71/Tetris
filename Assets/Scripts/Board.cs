using UnityEngine;
using UnityEngine.Tilemaps;
public class Board : MonoBehaviour
{
    public TetrominoData[] tetrominoes;
    public Tilemap tilemap {get ; private set;}
    public Piece activePiece {get; private set;}
    public Vector3Int spawnPosition;

    private void Awake()
    {
        tilemap = GetComponentInChildren<Tilemap>();
        for (int i = 0; i < tetrominoes.Length; i++)
        {
            tetrominoes[i].InitializeData();
        }
        activePiece = GetComponentInChildren<Piece>();
    }

    private void Start()
    {
        SpawnPiece();
    }

    public void SpawnPiece()
    {
        int random = Random.Range(0, tetrominoes.Length);
        TetrominoData tetrominoData = tetrominoes[random];
        
        this.activePiece.InitializePiece(this, spawnPosition, tetrominoData);
        SetPiece(activePiece);
    }

    public void SetPiece(Piece piece)
    {
        for (int i = 0; i < piece.cells.Length; i++)
        {
                                                        // offset
            Vector3Int tilePostition = piece.cells[i] + piece.position;
            tilemap.SetTile(tilePostition, piece.data.tile);
        }
    }
}