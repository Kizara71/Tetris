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

    private void OnEnable()
    {
        if (EventManager.Instance != null)
        {
            EventManager.Instance.OnPieceLocked += HandlePieceLocked;
        }
    }

    private void OnDisable()
    {
        if (EventManager.Instance != null)
        {
            EventManager.Instance.OnPieceLocked -= HandlePieceLocked;
        }
    }

    private void HandlePieceLocked()
    {
        ClearLines();
        SpawnPiece();
    }

    public void SpawnPiece()
    {
        int random = Random.Range(0, tetrominoes.Length);
        TetrominoData tetrominoData = tetrominoes[random];
        
        this.activePiece.InitializePiece(this, spawnPosition, tetrominoData);

        if (IsValidPosition(activePiece.cells, spawnPosition))
        {
            SetPiece(activePiece);
        }
        else
        {
            if (EventManager.Instance != null)
            {
                EventManager.Instance.TriggerGameOver();
            }
            GameOver();
        }
    }

    public void GameOver()
    {
        this.tilemap.ClearAllTiles();
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

    public bool IsValidPosition(Vector3Int[] cells, Vector3Int position)
    {

        for (int i = 0; i < cells.Length; i++)
        {
            Vector3Int tilePostition = cells[i] + position;
            
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

    public void ClearLines()
    {
        RectInt bounds = Bounds;
        int row = bounds.yMin;
        int linesCleared = 0;

        while(row < bounds.yMax)
        {
            if(IsLineFull(row))
            {
                ClearLine(row);
                linesCleared++;
            }
            else {row++;}
        }

        if (linesCleared > 0 && EventManager.Instance != null)
        {
            EventManager.Instance.TriggerLinesCleared(linesCleared);
        }
    }

    private bool IsLineFull(int row)
    {
        RectInt bounds = Bounds;
        for (int column = bounds.xMin; column < bounds.xMax; column++)
        {
            Vector3Int position = new Vector3Int(column, row, 0);
            if(!tilemap.HasTile(position))
            {
                return false;
            }
        }
        return true;
    }

    private void ClearLine(int row)
    {
        RectInt bounds = Bounds;
        for (int column = bounds.xMin; column < bounds.xMax; column++)
        {
            Vector3Int position = new Vector3Int(column, row, 0);
            tilemap.SetTile(position, null);
        }

        while (row < bounds.yMax)
        {
            for (int column = bounds.xMin; column < bounds.xMax; column++)
            {
                Vector3Int position = new Vector3Int(column, row + 1, 0);
                TileBase above = tilemap.GetTile(position);

                position = new Vector3Int(column, row, 0);
                tilemap.SetTile(position, above);
            }
            row++;
        }
    }
}