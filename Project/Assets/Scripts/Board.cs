using UnityEngine;
using UnityEngine.Tilemaps;

public class Board : MonoBehaviour
{
    public Tilemap tilemap { get; private set; }
    
    [SerializeField] private GameObject bombCount;
    private int bombsLeft;

    private void Awake()
    {
        tilemap = this.GetComponent<Tilemap>();
    }

    public void Draw(Cell[,] boardState)
    {
        int w = boardState.GetLength(0), h = boardState.GetLength(1);
        bombsLeft = GlobalVars.mineCount;

        for (int x = 0; x < w; x++)
        {
            for (int y = 0; y < h; y++)
            {
                Cell c = boardState[x, y];
                if(c.flagged) bombsLeft--;
                if(bombsLeft < 0) bombsLeft = 0;
                tilemap.SetTile(c.position, c.GetTile());
            }
        }

        bombCount.GetComponent<TextMesh>().text = $"Bombs: {bombsLeft}";
    }
}
