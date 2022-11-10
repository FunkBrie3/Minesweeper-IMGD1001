using UnityEngine;
using UnityEngine.Tilemaps;

public class Board : MonoBehaviour
{
    public Tilemap tilemap { get; private set; }
    private void Awake()
    {
        tilemap = this.GetComponent<Tilemap>();
    }

    public void Draw(Cell[,] boardState)
    {
        int w = boardState.GetLength(0), h = boardState.GetLength(1);

        for (int x = 0; x < w; x++)
        {
            for (int y = 0; y < h; y++)
            {
                Cell c = boardState[x, y];
                tilemap.SetTile(c.position, c.GetTile());
            }
        }
    }
}
