using UnityEngine;
using UnityEngine.Tilemaps;
public class Board : MonoBehaviour
{
    public TetrominoData[] tetrominoes;
    public Tilemap tilemap {get ; private set;}
    public Piece activePiece {get; private set;}
    public Vector3Int spawnPosition;
    public Vector2Int boardSize = new Vector2Int(10, 20);

    public RectInt Bounds
    {
        get
        {
            Vector2Int position = new Vector2Int(-boardSize.x / 2, -boardSize.y / 2);
            return new RectInt(position, boardSize);
        }
    }

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

    public void RemovePiece(Piece piece)
    {
        for (int i = 0; i < piece.cells.Length; i++)
        {
                                                        // offset
            Vector3Int tilePostition = piece.cells[i] + piece.position;
            tilemap.SetTile(tilePostition, null);
        }
    }

    public bool IsValidPosition(Piece piece , Vector3Int position)
    {

        for (int i = 0; i < piece.cells.Length; i++)
        {
            Vector3Int tilePostition = piece.cells[i] + position;
            
            if(!Bounds.Contains((Vector2Int)tilePostition))
            {
                return false;
            }


            if(tilemap.HasTile(tilePostition))
            {
                return false;
            }
        }

        return true;
    }
}