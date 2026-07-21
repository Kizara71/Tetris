using UnityEngine;
using UnityEngine.Tilemaps;

public class GhostPiece : MonoBehaviour
{
    public Tile tile;
    public Board mainBoard;
    public Piece trackingPiece;

    public Tilemap tilemap { get; private set; }
    public Vector3Int[] cells { get; private set; }
    public Vector3Int position { get; private set; }

    private void Awake()
    {
        tilemap = GetComponentInChildren<Tilemap>();
        cells = new Vector3Int[4];
    }

    private void LateUpdate()
    {
        RemoveGhostPiece();
        CopyPiece();
        DropGhostPiece();
        SetGhostPiece();
    }

    private void RemoveGhostPiece()
    {
        for (int i = 0; i < cells.Length; i++)
        {
            Vector3Int tilePosition = cells[i] + position;
            tilemap.SetTile(tilePosition, null);
        }
    }

    private void CopyPiece()
    {
        for (int i = 0; i < cells.Length; i++) 
        {
            cells[i] = trackingPiece.cells[i];
        }
    }

    private void DropGhostPiece()
    {
        Vector3Int position = trackingPiece.position;

        int current = position.y;
        int bottom = -mainBoard.boardSize.y / 2 - 1;

        mainBoard.RemovePiece(trackingPiece);

        for (int row = current; row >= bottom; row--)
        {
            position.y = row;

            if (mainBoard.IsValidPosition(trackingPiece.cells, position)) 
            {
                this.position = position;
            } 
            else 
            {
                break;
            }
        }

        mainBoard.SetPiece(trackingPiece);
    }

    private void SetGhostPiece()
    {
        for (int i = 0; i < cells.Length; i++)
        {
            Vector3Int tilePosition = cells[i] + position;
            tilemap.SetTile(tilePosition, tile);
        }
    }
}
