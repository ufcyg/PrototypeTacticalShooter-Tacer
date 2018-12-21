using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pathmap : MonoBehaviour
{
    public LayerMask unwalkalbeMask;
    public float nodeRadius;
    public aStarNode[,] pathmap;
    public bool rdyCk = false;

    float nodeDiameter;
    // int gridSizeX, gridSizeY;
    private int xSize, zSize;
    // Use this for initialization

    public bool debug = true;

    void Awake()
    {
        //GameObject UnitSelectionGO = GameObject.FindGameObjectWithTag("UnitSelection");
        xSize = transform.GetComponent<TileMapGraphics>().size_x;
        zSize = transform.GetComponent<TileMapGraphics>().size_z;
        nodeDiameter = nodeRadius * 2;
    }

    private void Start()
    {
        CreatePathmap();
        if (pathmap != null)
            rdyCk = true;
    }
    //private void Start()
    //{
    //    CreatePathmap();
    //    if (pathmap != null)
    //        rdyCk = true;
    //}

    public int MaxSize
    {
        get
        {
            return xSize * zSize;
        }
    }

    void CreatePathmap()
    {
        pathmap = new aStarNode[xSize, zSize];

        GameObject tileMapGO = GameObject.FindGameObjectWithTag("TileMap");
        TileMapData map = tileMapGO.GetComponent<TileMapGraphics>().map;
        for (int z = 0; z < zSize; z++)
        {
            for (int x = 0; x < xSize; x++)
            {
                pathmap[x, z] = new aStarNode(map.GetTile(x, z).walkable, map.GetTile(x, z).worldPos, x, z, map.GetTile(x,z).penalty);
            }
        }
        
        BlurPenaltyMap(0);
    }

    void BlurPenaltyMap(int blurSize)
    {
        int kernelSize = blurSize * 2 + 1;
        int kernelExtents = (kernelSize-1) / 2;

        int[,] penaltiesHorizontalPass = new int[xSize, zSize];
        int[,] penaltiesVerticalPass = new int[xSize, zSize];

        for (int y = 0; y < zSize; y++)
        {
            for (int x = -kernelExtents; x <= kernelExtents; x++)
            {
                int sampleX = Mathf.Clamp(x, 0, kernelExtents);
                penaltiesHorizontalPass[0, y] += pathmap[sampleX, y].movementPenalty;
            }

            for (int x = 1; x < xSize; x++)
            {
                int removeIndex = Mathf.Clamp(x - kernelExtents - 1, 0, xSize);
                int addIndex = Mathf.Clamp(x + kernelExtents, 0, xSize - 1);

                penaltiesHorizontalPass[x, y] = penaltiesHorizontalPass[x - 1, y] - pathmap[removeIndex, y].movementPenalty + pathmap[addIndex, y].movementPenalty;
            }
        }

        for (int x = 0; x < xSize; x++)
        {
            for (int y = -kernelExtents; y <= kernelExtents; y++)
            {
                int sampleY = Mathf.Clamp(x, 0, kernelExtents);
                penaltiesHorizontalPass[x, 0] += penaltiesHorizontalPass[x,sampleY];
            }

            for (int y = 1; y < zSize; y++)
            {
                int removeIndex = Mathf.Clamp(y - kernelExtents - 1, 0, zSize);
                int addIndex = Mathf.Clamp(y + kernelExtents, 0, zSize - 1);

                penaltiesVerticalPass[x, y] = penaltiesHorizontalPass[x, y-1] - penaltiesHorizontalPass[x, removeIndex] + penaltiesHorizontalPass[x, addIndex];
                int blurredPenalty = Mathf.RoundToInt((float)penaltiesVerticalPass[x, y] / (kernelSize * kernelSize));
                pathmap[x, y].movementPenalty = blurredPenalty;
            }
        }
    }

    public List<aStarNode> GetNeighbours(aStarNode node)
    {
        List<aStarNode> neighbours = new List<aStarNode>();

        for (int y = -1; y <= 1; y++)
        {
            for (int x = -1; x <= 1; x++)
            {
                if (x == 0 && y == 0)
                    continue;

                int checkX = node.gridX + x;
                int checkZ = node.gridZ + y;

                if (checkX >= 0 && checkX < xSize && checkZ >= 0 && checkZ < zSize)
                {
                    neighbours.Add(pathmap[checkX, checkZ]);
                }
            }
        }

        return neighbours;
    }

    public aStarNode NodeFromWorldPoint(Vector3 worldPosition)
    {
        int z = Mathf.FloorToInt(worldPosition.z);
        int x = Mathf.FloorToInt(worldPosition.x);
        return pathmap[x, z];

    }

    // Update is called once per frame
    void OnDrawGizmos()
    {
        Gizmos.DrawWireCube(transform.position + new Vector3(xSize / 2, 0, zSize / 2), new Vector3(xSize, 1, zSize));
        if (pathmap != null && debug)
        {
            foreach (aStarNode n in pathmap)
            {
                Gizmos.color = (n.walkable) ? Color.white : Color.red;
                Gizmos.DrawCube(n.worldPosition, Vector3.one * (nodeDiameter - .1f));
            }
        }
    }
}

public class aStarNode : IHeapItem<aStarNode>
{

    public bool walkable;
    public Vector3 worldPosition;
    public int movementPenalty;
    public int gCost;
    public int hCost;

    public int gridX;
    public int gridZ;

    public aStarNode parent;
    int heapIndex;

    public aStarNode(bool _walkable, Vector3 _worldPos, int _gridX, int _gridZ, int _penalty)
    {
        walkable = _walkable;
        worldPosition = _worldPos;
        gridX = _gridX;
        gridZ = _gridZ;
        movementPenalty = _penalty;
    }

    public int fCost
    {
        get
        {
            return gCost + hCost;
        }
    }

    public int HeapIndex
    {
        get
        {
            return heapIndex;
        }
        set
        {
            heapIndex = value;
        }
    }

    public int CompareTo(aStarNode nodeToCompare)
    {
        int compare = fCost.CompareTo(nodeToCompare.fCost);
        if (compare == 0)
        {
            compare = hCost.CompareTo(nodeToCompare.hCost);
        }
        return -compare;
    }
}