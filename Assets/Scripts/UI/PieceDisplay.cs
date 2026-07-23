using UnityEngine;
using UnityEngine.Tilemaps;

public class PieceDisplay : MonoBehaviour
{
    public Tilemap displayTilemap;
    
    // An offset to center the piece nicely in the UI grid
    public Vector3Int centerOffset = new Vector3Int(1, 1, 0); 

    public void DrawPiece(TetrominoData data)
    {
        if (displayTilemap == null) return;

        // Clear previous piece
        displayTilemap.ClearAllTiles();

        if (data.cells == null || data.cells.Length == 0) return;

        // Draw new piece
        for (int i = 0; i < data.cells.Length; i++)
        {
            Vector3Int tilePosition = (Vector3Int)data.cells[i] + centerOffset;
            displayTilemap.SetTile(tilePosition, data.tile);
        }
    }
}
