using UnityEngine;
public class Piece : MonoBehaviour
{
    public Board board {get; private set;}
    public TetrominoData data {get; private set;}
    public Vector3Int position {get; private set;}
    public Vector3Int[] cells {get; private set;}
    public int rotationIndex {get; private set;}


    public void InitializePiece(Board board, Vector3Int position, TetrominoData data)
    {
        this.board = board;
        this.position = position;
        this.data = data;

        this.rotationIndex = 0;

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
        // if(Input.GetKeyDown(KeyCode.W))
        // {
        //     Move(Vector2Int.up);
        // }
        if(Input.GetKeyDown(KeyCode.S))
        {
            Move(Vector2Int.down);
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

        board.SetPiece(this);
    }  

    private void RotatePiece(int rotationDirection)
    {
        int originalIndex = rotationIndex;
        rotationIndex = Wrap(rotationIndex + rotationDirection, 0, 4);
        
        ApplyRotationMatrix(rotationDirection);

        if(!TestWallKicks(originalIndex, rotationDirection))
        {
            rotationIndex = originalIndex;
            ApplyRotationMatrix(-rotationDirection);
        }
    }

    private void ApplyRotationMatrix(int rotationDirection)
    {
        for (int i = 0; i < cells.Length; i++)
        {
            Vector3 cell = cells[i];
            int x = 0, y = 0;

            switch (data.tetromino)
            {
                case Tetromino.I:
                    break;
                case Tetromino.O:
                    cell.x -= 0.5f;
                    cell.y -= 0.5f;
                    x = Mathf.CeilToInt((cell.x * Data.RotationMatrix[0]) + (cell.y * Data.RotationMatrix[1]));
                    y = Mathf.CeilToInt((cell.x * Data.RotationMatrix[2]) + (cell.y * Data.RotationMatrix[3]));
                    break;
                default:
                    x = Mathf.RoundToInt((cell.x * Data.RotationMatrix[0]) + (cell.y * Data.RotationMatrix[1]));
                    y = Mathf.RoundToInt((cell.x * Data.RotationMatrix[2]) + (cell.y * Data.RotationMatrix[3]));
                    break;
            }

            cells[i] = new Vector3Int(x, y, 0);
        }
    }

    private bool TestWallKicks(int rotationIndex, int rotationDirection)
    {
        int wallKickIndex = GetWallKickIndex(rotationIndex, rotationDirection);
        for (int i = 0; i < data.wallKicks.GetLength(1); i++)
        {
            Vector2Int translation = data.wallKicks[wallKickIndex , i];
            if(Move(translation))
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
    }

    private bool Move(Vector2Int translation)
    {
        Vector3Int newPostion = position;
        newPostion.x += translation.x;
        newPostion.y += translation.y;

        bool valid = board.IsValidPosition(this, newPostion);
        if(valid)
        {
            position = newPostion;
        }

        return valid; 
    }
}