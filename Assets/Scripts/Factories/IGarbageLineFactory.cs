using UnityEngine.Tilemaps;

public interface IGarbageLineFactory
{
    TileBase[][] CreateGarbageLines(int count, int width);
}
