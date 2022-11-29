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
        width = GlobalVars.width;
        height = GlobalVars.height;
        mineCount = GlobalVars.mineCount;
        board = GetComponentInChildren<Board>();

        GameObject bgBetter = new GameObject("bg");
        bgBetter.transform.parent = GameObject.FindGameObjectWithTag("MainCamera").transform;
        bgBetter.AddComponent<SpriteRenderer>().sprite =
            Resources.Load<Sprite>($"Sprite/{GlobalVars.GetThemeDirectory()}/TileUnknown");
        bgBetter.transform.localPosition = new Vector3(0, 0, 120);
        
        SpriteRenderer r = bgBetter.GetComponent<SpriteRenderer>();
        r.drawMode = SpriteDrawMode.Tiled;
        r.size = new Vector2(50, 30);
        
        for(int i = 0; i < 4; i++) {
            GameObject o = new GameObject($"line{i}");
            o.AddComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>($"Sprite/{GlobalVars.GetThemeDirectory()}/TileUnknown");
            o.GetComponent<SpriteRenderer>().color = new Color(0.5f, 0.5f, 0.5f);
            if(i < 2) {
            o.transform.position = new Vector3(i * width, (height / 2f), -9);
                o.transform.localScale = new Vector3(0.15f, height, 0);
            } else {
                o.transform.position = new Vector3((width / 2f), (i - 2) * height, -9);
                o.transform.localScale = new Vector3(width, 0.15f, 0);
            }
        }

        if(GlobalVars.isLined) {
            for(int j = 0; j < width - 1; j++) {
                GameObject o = new GameObject($"lineW{j}");
                o.AddComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>($"Sprite/{GlobalVars.GetThemeDirectory()}/TileUnknown");
                o.GetComponent<SpriteRenderer>().color = new Color(0.5f, 0, 0);

                o.transform.position = new Vector3((width / 2f), (j + 1), -8);
                o.transform.localScale = new Vector3(width, 0.05f, 0);
            }

            for(int k = 0; k < height - 1; k++) {
                GameObject o = new GameObject($"lineW{k}");
                o.AddComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>($"Sprite/{GlobalVars.GetThemeDirectory()}/TileUnknown");
                o.GetComponent<SpriteRenderer>().color = new Color(0.5f, 0, 0);

                o.transform.position = new Vector3((k + 1), (height / 2f), -8);
                o.transform.localScale = new Vector3(0.05f, height, 0);
            }
        } else {
            for(int j = 0; j < width; j++) {
                for(int k = 0; k < height; k++) {
                    bool col = (j + k) % 2 == 0;
                    if(col) {
                        GameObject o = new GameObject($"tile{j}/{k}");
                        o.AddComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Sprite/Square");
                        o.GetComponent<SpriteRenderer>().color = new Color(0, 0, 0, 0.2f);
                        o.transform.position = new Vector3(j + 0.5f, k + 0.5f, -8);
                    }
                }
            }
        }
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
        Camera.main.transform.position = new Vector3(width / 2f, height / 2f, -100);
        board.Draw(state);

        AudioManager.Play(AudioManager.AudioType.Pop);
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

        CheckWin();

        if (Input.GetKeyDown(KeyCode.R)) NewGame();
    }
    public void Reveal()
    {
        Vector3 worldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector3Int cellPos = board.tilemap.WorldToCell(worldPos);
        Cell c = GetCell(cellPos.x, cellPos.y);
        if (c.type == Cell.Type.Invalid || c.revealed || c.flagged) return;

        RevealCell(cellPos.x, cellPos.y, true);

        board.Draw(state);
    }
    private void RevealCell(int x, int y, bool initialCall)
    {
        state[x, y].revealed = true;
        if (initialCall)
            AudioManager.Play(AudioManager.AudioType.GrassBreak);
        if (state[x, y].type == Cell.Type.Mine) Explode(x, y);
        if (state[x, y].type == Cell.Type.Empty)
        {
            for (int xp = -1; xp <= 1; xp++)
            {
                for (int yp = -1; yp <= 1; yp++)
                {
                    int xpp = x + xp, ypp = y + yp;
                    if (ValidCell(xpp, ypp) && !(xpp == x && ypp == y) && state[xpp, ypp].revealed == false) RevealCell(xpp, ypp, false);
                }
            }
        }
    }
    private void Explode(int x, int y)
    {
        AudioManager.Play(AudioManager.AudioType.Explode);
        gameOver = true;
        state[x, y].exploded = true;

        for(int i = 0; i < 5; i++) {
            GameObject o = Resources.Load<GameObject>("Prefab/particleExplode");
            o.transform.position = Camera.main.ScreenToWorldPoint(Input.mousePosition) + new Vector3(Random.Range(-2.5f, 2.5f), Random.Range(-2.5f, 2.5f), 0);
            o.transform.position = new Vector3(o.transform.position.x, o.transform.position.y, -10);
            Instantiate(o);
        }

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
        AudioManager.Play(AudioManager.AudioType.WoodClick);
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
        if(gameOver) return;
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                Cell c = state[x, y];
                if (c.type != Cell.Type.Mine && !c.revealed) return;
            }
        }

        gameOver = true;
        AudioManager.Play(AudioManager.AudioType.ChallengeComplete);
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                if (state[x, y].type == Cell.Type.Mine) 
                {
                    state[x, y].flagged = true;
                }
            }
        }
        board.Draw(state);
    }
}
