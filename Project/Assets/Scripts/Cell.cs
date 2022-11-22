using UnityEngine;
using UnityEngine.Tilemaps;

public struct Cell
{
    public enum Type
    {
        Invalid, Empty, Mine, Number
    }
    public Type type;
    public Vector3Int position;
    public int number;
    public bool revealed, flagged, exploded;

    private Tile LoadRsrc(string id) {
        //return Resources.Load<Tile>("Tiles/Tile" + id);
        return Resources.Load<Tile>("Tiles/MCTile/Tile" + id);
    }
    public Tile GetTile() {
        if (revealed)
        {
            switch (type) {
                case Type.Empty: return LoadRsrc("Empty");
                case Type.Mine:
                    if (exploded) return LoadRsrc("Exploded");
                    else return LoadRsrc("Mine");
                case Type.Number: return LoadRsrc("" + number);
                default: return null;
            }
        }
        else if (flagged) return LoadRsrc("Flag");
        else return LoadRsrc("Unknown");
    }
}
