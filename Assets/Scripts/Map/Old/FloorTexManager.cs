using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorTexManager : MonoBehaviour {

    public float tileSize = 1.0f;

    public Texture2D terrainTiles;
    public int tileResolution;

    Grid grid;
    MeshRenderer meshRenderer;

    private void Awake()
    {
        transform.GetComponent<FloorTexManager>().enabled = false;
    }

    private void Start()
    {
        GameObject gridGO = GameObject.Find("GameBoard(Clone)");
        grid = gridGO.GetComponent<Grid>();
    }

    void Update()
    { 
        if(Input.GetKeyDown("space"))
            BuildTexture();
    }

    Color[][] ChopUpTiles()
    {
        int numTilesPerRow = terrainTiles.width / tileResolution;
        int numRows = terrainTiles.height / tileResolution;

        Color[][] tiles = new Color[numTilesPerRow* numRows][];

        for (int y = 0; y < numRows; y++)
        {
            for (int x = 0; x < numTilesPerRow; x++)
            {
                tiles[y * numTilesPerRow + x] = terrainTiles.GetPixels(x * tileResolution, y * tileResolution, tileResolution, tileResolution);
            }
        }
        return tiles;
    }

    void BuildTexture()
    {
        
        int texWidth = grid.xSize * tileResolution;
        int texHeight = grid.zSize * tileResolution;
        Texture2D texture = new Texture2D(texWidth, texHeight);

        Color[][] tiles = ChopUpTiles();

        for (int y = 0; y < grid.zSize; y++)
        {
            for (int x = 0; x < grid.xSize; x++)
            {
                Color[] p = tiles[Random.Range(0, 4)];
                texture.SetPixels(x*tileResolution,y*tileResolution,tileResolution,tileResolution,p);
            }
        }
        texture.Apply();

        meshRenderer = grid.GetComponent<MeshRenderer>();
        meshRenderer.sharedMaterials[0].mainTexture = texture;
    }
}
