using UnityEngine;
using UnityEngine.Tilemaps;
public class Board : MonoBehaviour
{
    private IPieceFactory pieceFactory;
    private IGarbageLineFactory garbageFactory;
    private NextQueueSystem nextQueueSystem;
    private HoldSystem holdSystem;

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
        pieceFactory = GetComponent<IPieceFactory>();
        garbageFactory = GetComponent<IGarbageLineFactory>();
        nextQueueSystem = GetComponent<NextQueueSystem>();
        holdSystem = GetComponent<HoldSystem>();
        
        if (pieceFactory == null)
        {
            Debug.LogError("Board requires an IPieceFactory component!");
        }

        activePiece = GetComponentInChildren<Piece>();
    }

    private void Start()
    {
        if (nextQueueSystem != null)
        {
            nextQueueSystem.InitializeQueue();
        }
        SpawnPiece();
        StartCoroutine(TempGarbageRoutine()); // Temporary for testing
    }

    private System.Collections.IEnumerator TempGarbageRoutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(10f);
            AddGarbageLines(4);
        }
    }

    // Call this method when enemies or events trigger garbage lines
    public void AddGarbageLines(int linesToAdd)
    {
        if (garbageFactory == null) return;

        // Temporarily remove the active piece so its tiles don't get shifted or leave ghosts
        if (activePiece != null)
        {
            RemovePiece(activePiece);
        }
        
        RectInt bounds = Bounds;

        // 1. Shift all current tiles UP by 'linesToAdd'
        for (int row = bounds.yMax - 1; row >= bounds.yMin; row--)
        {
            for (int col = bounds.xMin; col < bounds.xMax; col++)
            {
                Vector3Int pos = new Vector3Int(col, row, 0);
                TileBase tile = tilemap.GetTile(pos);
                
                // Only move if there is a tile
                if (tile != null)
                {
                    Vector3Int newPos = new Vector3Int(col, row + linesToAdd, 0);
                    tilemap.SetTile(newPos, tile);
                    tilemap.SetTile(pos, null);
                }
            }
        }

        // 2. Insert new garbage lines at the bottom
        TileBase[][] newGarbageLines = garbageFactory.CreateGarbageLines(linesToAdd, bounds.size.x);
        for (int i = 0; i < linesToAdd; i++)
        {
            for (int col = 0; col < bounds.size.x; col++)
            {
                Vector3Int pos = new Vector3Int(bounds.xMin + col, bounds.yMin + i, 0);
                tilemap.SetTile(pos, newGarbageLines[i][col]);
            }
        }

        // 3. Shift the active piece's logical position up and redraw it
        if (activePiece != null)
        {
            activePiece.ShiftUp(linesToAdd);
            SetPiece(activePiece);
        }
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
        if (holdSystem != null) holdSystem.ResetHoldLock();
        ClearLines();
        SpawnPiece();
    }

    public void HoldPiece()
    {
        if (holdSystem == null) return;
        if (holdSystem.hasHeldThisTurn) return;

        RemovePiece(activePiece);
        
        TetrominoData? swappedPiece = holdSystem.HoldPiece(activePiece.data);
        
        if (swappedPiece.HasValue)
        {
            // Spawn the previously held piece
            activePiece.InitializePiece(this, spawnPosition, swappedPiece.Value);
            SetPiece(activePiece);
        }
        else
        {
            // First time holding, just spawn a new piece from the queue
            SpawnPiece();
        }
    }

    public void SpawnPiece()
    {
        if (pieceFactory == null) return;

        TetrominoData tetrominoData;
        if (nextQueueSystem != null)
        {
            tetrominoData = nextQueueSystem.GetNextPiece();
        }
        else
        {
            tetrominoData = pieceFactory.CreatePiece();
        }

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