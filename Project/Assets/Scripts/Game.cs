using UnityEngine;
using UnityEngine.SceneManagement;

public class Game : MonoBehaviour
{
    [SerializeField] private int width = 6, height = 6, mineCount = 4;
    private Board board;
    private Cell[,] state;
    private bool gameOver;
    private void Awake()
    {
        board = GetComponentInChildren<Board>();
    }

    private void Start()
    {
        NewGame();
    }

    private void NewGame()
    {
        gameOver = false;
        state = new Cell[width, height];
        GenerateCells();
        GenerateMines();
        GenerateNumbers();
        Camera.main.transform.position = new Vector3(width / 2f, height / 2f, -10);
        board.Draw(state);
    }

    private void GenerateCells() {
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                Cell c = new Cell();
                c.position = new Vector3Int(x, y, 0);
                c.type = Cell.Type.Empty;
                state[x, y] = c;
            }
        }
    }

    private void GenerateMines()
    {
        for (int i = 0; i < mineCount; i++)
        {
            if (i + 1 == width * height) return;

            int x = Random.Range(0, width), y = Random.Range(0, height);
            if (state[x, y].type == Cell.Type.Mine) i--;
            state[x, y].type = Cell.Type.Mine;
        }
    }

    private void GenerateNumbers()
    {
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                Cell c = state[x, y];
                if (c.type == Cell.Type.Mine) continue;
                int num = 0;
                for (int xp = -1; xp <= 1; xp++)
                {
                    for (int yp = -1; yp <= 1; yp++)
                    {
                        int xpp = x + xp, ypp = y + yp;
                        if (ValidCell(xpp, ypp) &&
                            state[xpp, ypp].type == Cell.Type.Mine) num++;
                    }
                }
                if (num > 0)
                {
                    c.type = Cell.Type.Number;
                    c.number = num;
                }

                state[x, y] = c;
            }
        }
    }

    private void Update()
    {
        if (!gameOver)
        {
            if (Input.GetMouseButtonDown(1))
            {
                Flag();
            }
            else if (Input.GetMouseButtonUp(0))
            {
                Reveal();
            }
        }

        if (Input.GetKeyDown(KeyCode.R)) NewGame();
        if (Input.GetKeyDown(KeyCode.Escape)) SceneManager.LoadScene("menu");
    }
    public void Reveal()
    {
        Vector3 worldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector3Int cellPos = board.tilemap.WorldToCell(worldPos);
        Cell c = GetCell(cellPos.x, cellPos.y);
        if (c.type == Cell.Type.Invalid || c.revealed || c.flagged) return;

        RevealCell(cellPos.x, cellPos.y);

        board.Draw(state);
    }
    private void RevealCell(int x, int y)
    {
        state[x, y].revealed = true;
        if (state[x, y].type == Cell.Type.Mine) Explode(x, y);
        if (state[x, y].type == Cell.Type.Empty)
        {
            for (int xp = -1; xp <= 1; xp++)
            {
                for (int yp = -1; yp <= 1; yp++)
                {
                    int xpp = x + xp, ypp = y + yp;
                    if (ValidCell(xpp, ypp) && !(xpp == x && ypp == y) && state[xpp, ypp].revealed == false) RevealCell(xpp, ypp);
                }
            }
        }
    }
    private void Explode(int x, int y)
    {
        gameOver = true;
        state[x, y].exploded = true;

        for (int xp = 0; xp < width; xp++)
        {
            for (int yp = 0; yp < height; yp++)
            {
                if (state[xp, yp].type == Cell.Type.Mine) state[xp, yp].revealed = true;
            }
        }
    }
    private void Flag()
    {
        Vector3 worldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector3Int cellPos = board.tilemap.WorldToCell(worldPos);
        Cell c = GetCell(cellPos.x, cellPos.y);
        if (c.type == Cell.Type.Invalid) return;

        c.flagged = !c.flagged;
        state[cellPos.x, cellPos.y] = c;
        board.Draw(state);
    }
    private Cell GetCell(int x, int y) {
        if (ValidCell(x, y)) return state[x, y];
        else return new Cell();
    }
    private bool ValidCell(int x, int y)
    {
        return 0 <= x && x < width && 0 <= y && y < height;
    }
    private void CheckWin()
    {
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                Cell c = state[x, y];
                if (c.type != Cell.Type.Mine && !c.revealed) return;
            }
        }

        gameOver = true;
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                if (state[x, y] .type == Cell.Type.Mine) 
                {
                    state[x, y].flagged = true;
                }
            }
        }
    }
}
