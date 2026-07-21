using UnityEngine;
public class Piece : MonoBehaviour
{
    public Board board {get; private set;}
    public TetrominoData data {get; private set;}
    public Vector3Int position {get; private set;}
    public Vector3Int[] cells {get; private set;}
    public int rotationIndex {get; private set;}

    public float stepDelay = 1f;
    public float lockDelay = 0.5f;
    public float softDropDelay = 0.1f;

    private float stepTimer = 0f;
    private float lockTimer = 0f;
    private float softDropTimer = 0f;


    public void InitializePiece(Board board, Vector3Int position, TetrominoData data)
    {
        this.board = board;
        this.position = position;
        this.data = data;

        this.rotationIndex = 0;

        this.stepTimer = Time.time + stepDelay;
        this.lockTimer = 0f;
        this.softDropTimer = Time.time + softDropDelay;


        if(cells == null)
        {
            cells = new Vector3Int[data.cells.Length];  
        }

        for (int i = 0; i < data.cells.Length; i++)
        {
            cells[i] = (Vector3Int)data.cells[i];
        }
    }

    public void Update()
    {
        board.RemovePiece(this);

        lockTimer += Time.deltaTime;

        // if(Input.GetKeyDown(KeyCode.W))
        // {
        //     Move(Vector2Int.up);
        // }
        if(Input.GetKey(KeyCode.S) && Time.time >= softDropTimer)
        {
            Move(Vector2Int.down);
            softDropTimer = Time.time + softDropDelay;
        }
        if(Input.GetKeyDown(KeyCode.A))
        {
            Move(Vector2Int.left);
        }
        if(Input.GetKeyDown(KeyCode.D))
        {
            Move(Vector2Int.right);
        }
        if(Input.GetKeyDown(KeyCode.Space))
        {
            HardDrop();
        }

        if(Input.GetKeyDown(KeyCode.Q))
        {
            RotatePiece(-1);
        }
        if(Input.GetKeyDown(KeyCode.E))
        {
            RotatePiece(1);
        }

        if(Time.time >= stepTimer)
        {
            Step();
        }

        board.SetPiece(this);
    }  

    private void Step()
    {
        this.stepTimer = Time.time + stepDelay; 
        Move(Vector2Int.down);
        if(lockTimer >= lockDelay)
        {
            Lock();
        }
    }

    private void Lock()
    {
        board.SetPiece(this);
        board.ClearLines();
        board.SpawnPiece();    
    }

    private void RotatePiece(int rotationDirection)
    {
        int originalIndex = rotationIndex;
        int newRotationIndex = Wrap(rotationIndex + rotationDirection, 0, 4);
        
        Vector3Int[] newCells = new Vector3Int[cells.Length];
        ApplyRotationMatrix(rotationDirection, newCells);

        if(!TestWallKicks(newCells, originalIndex, rotationDirection))
        {
            // Rotation failed, do nothing.
        }
        else
        {
            // Rotation succeeded, apply changes.
            this.rotationIndex = newRotationIndex;
            this.cells = newCells;
        }
    }

    private void ApplyRotationMatrix(int rotationDirection, Vector3Int[] newCells)
    {
        for (int i = 0; i < cells.Length; i++)
        {
            Vector3 cell = cells[i];

            switch (data.tetromino)
            {
                case Tetromino.I:
                case Tetromino.O:
                    cell.x -= 0.5f;
                    cell.y -= 0.5f;
                    break;
                default:
                    break;
            }

            float x, y;

            // Apply rotation matrix
            x = (cell.x * Data.RotationMatrix[0] * rotationDirection) + (cell.y * Data.RotationMatrix[1] * rotationDirection);
            y = (cell.x * Data.RotationMatrix[2] * rotationDirection) + (cell.y * Data.RotationMatrix[3] * rotationDirection);

            // Use Ceiling for I and O pieces as they rotate around a center point, not a center cell
            // Use Round for all other pieces
            newCells[i] = (data.tetromino == Tetromino.I || data.tetromino == Tetromino.O) ? new Vector3Int(Mathf.CeilToInt(x), Mathf.CeilToInt(y), 0) : new Vector3Int(Mathf.RoundToInt(x), Mathf.RoundToInt(y), 0);
        }
    }

    private bool TestWallKicks(Vector3Int[] newCells, int rotationIndex, int rotationDirection)
    {
        int wallKickIndex = GetWallKickIndex(rotationIndex, rotationDirection);
        for (int i = 0; i < data.wallKicks.GetLength(1); i++)
        {
            Vector3Int translation = (Vector3Int)data.wallKicks[wallKickIndex , i];
            if(Move(newCells, translation))
            {
                return true;
            }
        }
        return false;
    }

    private int GetWallKickIndex(int rotationIndex, int rotationDirection)
    {
        int wallKickIndex = rotationIndex * 2;
        if(rotationDirection < 0)
        {
            wallKickIndex--;
        }
        return Wrap(wallKickIndex, 0, data.wallKicks.GetLength(0));
    }

    private int Wrap(int input , int min , int max)
    {
        if(input < min)
        {
            return max - (min - input) % (max - min);
        }
        else
        {
            return min + (input - min) % (max - min);
        }
    }

    private void HardDrop()
    {
        while(Move(Vector2Int.down))
        {
            continue;
        }
        Lock();
    }

    private bool Move(Vector3Int[] newCells, Vector3Int translation)
    {
        Vector3Int newPosition = position + translation;

        bool valid = board.IsValidPosition(newCells, newPosition);
        if(valid)
        {
            position = newPosition;
            lockTimer = 0f;
        }

        return valid; 
    }

    private bool Move(Vector2Int translation)
    {
        Vector3Int newPosition = position;
        newPosition.x += translation.x;
        newPosition.y += translation.y;
        bool valid = board.IsValidPosition(this.cells, newPosition);
        if(valid)
        {
            position = newPosition;
            lockTimer = 0f;
        }

        return valid; 
    }
}