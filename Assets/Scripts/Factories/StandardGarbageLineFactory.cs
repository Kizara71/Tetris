using UnityEngine;
using UnityEngine.Tilemaps;

public class StandardGarbageLineFactory : MonoBehaviour, IGarbageLineFactory
{
    [Tooltip("The tile to use for garbage blocks")]
    [SerializeField] private TileBase garbageTile;

    public TileBase[][] CreateGarbageLines(int count, int width)
    {
        TileBase[][] lines = new TileBase[count][];
        
        // Randomly pick one column to be the empty hole for ALL of these lines
        int holeIndex = Random.Range(0, width);

        for (int i = 0; i < count; i++)
        {
            lines[i] = new TileBase[width];
            for (int col = 0; col < width; col++)
            {
                if (col == holeIndex)
                {
                    lines[i][col] = null; // The hole
                }
                else
                {
                    lines[i][col] = garbageTile; // The garbage block
                }
            }
        }
        return lines;
    }
}
