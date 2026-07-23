using UnityEngine.Tilemaps;

namespace Tetris
{
    public interface IGarbageLineFactory
    {
        TileBase[][] CreateGarbageLines(int count, int width);
    }
    
}

