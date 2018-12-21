using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardManager : MonoBehaviour
{
    public GameObject tilemapGO;
    public GameObject GridSelection;
    public Vector3 mousePos;
    private List<SelectableUnit> selectedObjects = null;

    private GameObject tileMap;

    void Awake()
    {
        tileMap = (GameObject)Instantiate(tilemapGO);
        tileMap.transform.parent = gameObject.transform;
    }

    private void Update()
    {
        GameObject UnitSelectionGO = GameObject.FindGameObjectWithTag("UnitSelection");
        selectedObjects = UnitSelectionGO.GetComponent<UnitSelection>().GetSelectedObjects();
        if (selectedObjects != null && selectedObjects.Count != 0)
        {
            RaycastHit vHit = new RaycastHit();
            Ray vRay = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(vRay, out vHit, 1000))
            {
                TileMapData map = tileMap.GetComponent<TileMapGraphics>().map;
                //int sizeX = tileMap.GetComponent<TileMapGraphics>().size_x;
                //int sizeZ = tileMap.GetComponent<TileMapGraphics>().size_z;
                Vector3 meshHitPos = vHit.point;
                var vec = meshHitPos;
                int xVecHit = Mathf.FloorToInt(vec.x);
                int zVecHit = Mathf.FloorToInt(vec.z);
                mousePos = map.GetTile(xVecHit, zVecHit).worldPos;
                if (GameObject.Find("SelectionGridPrefab(Clone)") == null)
                {
                    GameObject GridSelectionGO = (GameObject)Instantiate(GridSelection, mousePos, Quaternion.Euler(270, 0, 0));
                    GridSelectionGO.transform.parent = gameObject.transform;
                }
                else
                {
                    Destroy(GameObject.Find("SelectionGridPrefab(Clone)"));
                    GameObject GridSelectionGO = (GameObject)Instantiate(GridSelection, mousePos, Quaternion.Euler(270, 0, 0));
                    GridSelectionGO.transform.parent = gameObject.transform;
                }
            }
        }
        else
        {
            if (GameObject.Find("SelectionGridPrefab(Clone)") != null)
                Destroy(GameObject.Find("SelectionGridPrefab(Clone)"));
        }
    }
}