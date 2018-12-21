using UnityEngine;
public class TileData
{
    public enum TYPE
    {
        GRASS,
        MUD,
        STREET,
        GRAVEL,
        SAND,
        lowWATER,
        deepWATER,
        UNKNOWN
    }

    public int c;
    public int penalty;
    public bool walkable;
    public Vector3 worldPos;

    public TileData(TYPE enumtype)
    {
        if (enumtype == TYPE.GRASS)
        {
            c = 0;
            penalty = 10;
            walkable = true;
        }
        if (enumtype == TYPE.MUD)
        {
            c = 1;
            penalty = 50;
            walkable = true;
        }
        if (enumtype == TYPE.STREET)
        {
            c = 2;
            penalty = 0;
            walkable = true;
        }
        if (enumtype == TYPE.GRAVEL)
        {
            c = 3;
            penalty = 30;
            walkable = true;
        }
        if (enumtype == TYPE.SAND)
        {
            c = 4;
            penalty = 30;
            walkable = true;
        }
        if (enumtype == TYPE.lowWATER)
        {
            c = 5;
            penalty = 40;
            walkable = true;
        }
        if (enumtype == TYPE.deepWATER)
        {
            c = 6;
            penalty = 0;
            walkable = false;
        }
        if (enumtype == TYPE.UNKNOWN)
        {
            c = 7;
            penalty = 0;
            walkable = true;
        }
    }

    public void SetWorldPos(Vector3 _worldPos)
    {
        worldPos = _worldPos;
    }
}