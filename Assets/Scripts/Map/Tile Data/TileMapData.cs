﻿public class TileMapData
{

    TileData[,] _tiles;
    int _width;
    int _height;

    //int[,] map_data;

    //public TileMapData()
    //{
    //    TileMapData(20,20);
    //}

    public TileMapData(int width, int height)
    {
        _width = width;
        _height = height;

        _tiles = new TileData[_width ,_height];
        //map_data = new int[_width, _height];

        for (int y = 0; y<height;y++)
        //for (int y = height-1; y >= 0; y--)
        {
            for (int x = 0; x < width; x++)
            {
                    _tiles[x,y] = new TileData(TileData.TYPE.GRASS);
            }
        }

        GenerateTestMap();
    }

    //public int GetTileAt(int x, int y)
    //{
    //    return map_data[x, y];
    //}

    public TileData GetTile(int x, int y)
    {
        if (x < 0 || x >= _width || y < 0 || y >= _height)
            return null;

        return _tiles[x,y];
    }

    private void GenerateTestMap()
    {
        //lake 1
        _tiles[3, 3] = new TileData(TileData.TYPE.deepWATER);
        _tiles[3, 2] = new TileData(TileData.TYPE.deepWATER);
        _tiles[2, 3] = new TileData(TileData.TYPE.deepWATER);
        _tiles[2, 2] = new TileData(TileData.TYPE.deepWATER);
        _tiles[1, 1] = new TileData(TileData.TYPE.SAND);
        _tiles[1, 2] = new TileData(TileData.TYPE.SAND);
        _tiles[2, 1] = new TileData(TileData.TYPE.SAND);
        _tiles[1, 3] = new TileData(TileData.TYPE.SAND);
        _tiles[3, 1] = new TileData(TileData.TYPE.SAND);
        _tiles[1, 4] = new TileData(TileData.TYPE.SAND);
        _tiles[4, 1] = new TileData(TileData.TYPE.SAND);
        _tiles[4, 2] = new TileData(TileData.TYPE.SAND);
        _tiles[4, 3] = new TileData(TileData.TYPE.SAND);
        _tiles[4, 4] = new TileData(TileData.TYPE.SAND);
        _tiles[3, 4] = new TileData(TileData.TYPE.SAND);
        _tiles[2, 4] = new TileData(TileData.TYPE.SAND);
        _tiles[1, 4] = new TileData(TileData.TYPE.SAND);

        //street
        for (int x = 0; x < _height; x++)
        {
            if(x<_height/2)
                _tiles[5, x] = new TileData(TileData.TYPE.GRAVEL);
            _tiles[6, x] = new TileData(TileData.TYPE.STREET);
            _tiles[7, x] = new TileData(TileData.TYPE.STREET);
        }
        //mud
        _tiles[5, 8] = new TileData(TileData.TYPE.MUD);
        _tiles[6, 8] = new TileData(TileData.TYPE.MUD);
        _tiles[7, 8] = new TileData(TileData.TYPE.MUD);
        _tiles[8, 8] = new TileData(TileData.TYPE.MUD);
        //river
        _tiles[0, 8] = new TileData(TileData.TYPE.lowWATER);
        _tiles[1, 8] = new TileData(TileData.TYPE.lowWATER);
        _tiles[2, 8] = new TileData(TileData.TYPE.lowWATER);
        _tiles[3, 8] = new TileData(TileData.TYPE.lowWATER);
        _tiles[4, 8] = new TileData(TileData.TYPE.lowWATER);
        _tiles[4, 9] = new TileData(TileData.TYPE.lowWATER);
        _tiles[4, 10] = new TileData(TileData.TYPE.lowWATER);
        _tiles[4, 11] = new TileData(TileData.TYPE.lowWATER);
    }
}



//using UnityEngine;
//using System.Collections.Generic;

//public class TileMapData
//{

//    /*protected class DTile {
//		bool isWalkable = false;
//		int tileGraphicId = 0;
//		string name = "Unknown";
//	}

//	List<DTile> tileTypes;

//	void InitTiles() {
//		tileType[1].name = "Floor";
//		tileType[1].isWalkable = true;
//		tileType[1].tileGraphicId = 1;
//		tileType[1].damagePerTurn = 0;
//	}*/

//    protected class DRoom
//    {
//        public int left;
//        public int top;
//        public int width;
//        public int height;

//        public bool isConnected = false;

//        public int right
//        {
//            get { return left + width - 1; }
//        }

//        public int bottom
//        {
//            get { return top + height - 1; }
//        }

//        public int center_x
//        {
//            get { return left + width / 2; }
//        }

//        public int center_y
//        {
//            get { return top + height / 2; }
//        }

//        public bool CollidesWith(DRoom other)
//        {
//            if (left > other.right - 1)
//                return false;

//            if (top > other.bottom - 1)
//                return false;

//            if (right < other.left + 1)
//                return false;

//            if (bottom < other.top + 1)
//                return false;

//            return true;
//        }


//    }

//    int size_x;
//    int size_y;

//    int[,] map_data;

//    List<DRoom> rooms;

//    /*
//	 * 0 = unknown
//	 * 1 = floor
//	 * 2 = wall
//	 * 3 = stone
//	 */

//    public TileMapData(int size_x, int size_y)
//    {
//        DRoom r;
//        this.size_x = size_x;
//        this.size_y = size_y;

//        map_data = new int[size_x, size_y];

//        for (int x = 0; x < size_x; x++)
//        {
//            for (int y = 0; y < size_y; y++)
//            {
//                map_data[x, y] = 3;
//            }
//        }

//        rooms = new List<DRoom>();

//        int maxFails = 10;

//        while (rooms.Count < 10)
//        {
//            int rsx = Random.Range(4, 14);
//            int rsy = Random.Range(4, 10);

//            r = new DRoom();
//            r.left = Random.Range(0, size_x - rsx);
//            r.top = Random.Range(0, size_y - rsy);
//            r.width = rsx;
//            r.height = rsy;

//            if (!RoomCollides(r))
//            {
//                rooms.Add(r);
//            }
//            else
//            {
//                maxFails--;
//                if (maxFails <= 0)
//                    break;
//            }

//        }

//        foreach (DRoom r2 in rooms)
//        {
//            MakeRoom(r2);
//        }


//        for (int i = 0; i < rooms.Count; i++)
//        {
//            if (!rooms[i].isConnected)
//            {
//                int j = Random.Range(1, rooms.Count);
//                MakeCorridor(rooms[i], rooms[(i + j) % rooms.Count]);
//            }
//        }

//        MakeWalls();

//    }

//    bool RoomCollides(DRoom r)
//    {
//        foreach (DRoom r2 in rooms)
//        {
//            if (r.CollidesWith(r2))
//            {
//                return true;
//            }
//        }

//        return false;
//    }

//    public int GetTileAt(int x, int y)
//    {
//        return map_data[x, y];
//    }

//    void MakeRoom(DRoom r)
//    {

//        for (int x = 0; x < r.width; x++)
//        {
//            for (int y = 0; y < r.height; y++)
//            {
//                if (x == 0 || x == r.width - 1 || y == 0 || y == r.height - 1)
//                {
//                    map_data[r.left + x, r.top + y] = 2;
//                }
//                else
//                {
//                    map_data[r.left + x, r.top + y] = 1;
//                }
//            }
//        }

//    }

//    void MakeCorridor(DRoom r1, DRoom r2)
//    {
//        int x = r1.center_x;
//        int y = r1.center_y;

//        while (x != r2.center_x)
//        {
//            map_data[x, y] = 1;

//            x += x < r2.center_x ? 1 : -1;
//        }

//        while (y != r2.center_y)
//        {
//            map_data[x, y] = 1;

//            y += y < r2.center_y ? 1 : -1;
//        }

//        r1.isConnected = true;
//        r2.isConnected = true;

//    }

//    void MakeWalls()
//    {
//        for (int x = 0; x < size_x; x++)
//        {
//            for (int y = 0; y < size_y; y++)
//            {
//                if (map_data[x, y] == 3 && HasAdjacentFloor(x, y))
//                {
//                    map_data[x, y] = 2;
//                }
//            }
//        }
//    }

//    bool HasAdjacentFloor(int x, int y)
//    {
//        if (x > 0 && map_data[x - 1, y] == 1)
//            return true;
//        if (x < size_x - 1 && map_data[x + 1, y] == 1)
//            return true;
//        if (y > 0 && map_data[x, y - 1] == 1)
//            return true;
//        if (y < size_y - 1 && map_data[x, y + 1] == 1)
//            return true;

//        if (x > 0 && y > 0 && map_data[x - 1, y - 1] == 1)
//            return true;
//        if (x < size_x - 1 && y > 0 && map_data[x + 1, y - 1] == 1)
//            return true;

//        if (x > 0 && y < size_y - 1 && map_data[x - 1, y + 1] == 1)
//            return true;
//        if (x < size_x - 1 && y < size_y - 1 && map_data[x + 1, y + 1] == 1)
//            return true;


//        return false;
//    }
//}
