using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshFilter), typeof(MeshRenderer), typeof(MeshCollider))]
public class Grid : MonoBehaviour
{

    public int xSize, zSize;
    public bool rdyCk = false;

    private Vector3[] gridFields;
    private int xNorm, zNorm;
    public Mesh mesh;
    private int fieldCounter = 0;
    

    void Start()
    {
        if (xSize > 100)
        {
            xSize = 100;
        }
        if (zSize > 100)
        {
            zSize = 100;
        }
        //StartCoroutine(Generate());
        Generate();
        if (mesh != null)
            rdyCk = true;
    }

    private Vector3[] vertices;

    //private IEnumerator Generate()
    private void Generate()
    {
        //WaitForSeconds wait = new WaitForSeconds(0.05f);

        GetComponent<MeshFilter>().mesh = mesh = new Mesh();
        mesh.name = "Procedural Grid";

        this.gridFields = new Vector3[xSize * zSize];

        vertices = new Vector3[(xSize + 1) * (zSize + 1)];
        Vector2[] uv = new Vector2[vertices.Length];
        zNorm = zSize / 2;
        xNorm = xSize / 2;
        for (int i = 0, z = 0-zNorm; z <= zNorm; z++)
        {
            for (int x = 0-xNorm; x <= xNorm; x++, i++)
            {
                vertices[i] = new Vector3(x, 0, z);
                uv[i] = new Vector2((float)x / xSize, (float)z / zSize);

                if (z != zNorm && x != xNorm)
                {
                    this.gridFields[fieldCounter] = vertices[i] + new Vector3(0.5f, -0.5f, 0.5f);
                    fieldCounter++;
                }
            }
        }
        //Debug.Log(fieldCounter);
        mesh.vertices = vertices;
        mesh.uv = uv;

        int[] triangles = new int[xSize * zSize * 6];
        for (int ti = 0, vi = 0, y = 0; y < zSize; y++, vi++)
        {
            for (int x = 0; x < xSize; x++, ti += 6, vi++)
            {
                triangles[ti] = vi;
                triangles[ti + 3] = triangles[ti + 2] = vi + 1;
                triangles[ti + 4] = triangles[ti + 1] = vi + xSize + 1;
                triangles[ti + 5] = vi + xSize + 2;
                //yield return wait;
            }
        }
        mesh.triangles = triangles;
        mesh.RecalculateNormals();

        MeshCollider mc = this.GetComponent<MeshCollider>();
        mc.sharedMesh = mesh;
    }

    private void OnDrawGizmos()
    {
        if (vertices == null)
        {
            return;
        }
        Gizmos.color = Color.black;
        for (int i = 0; i < vertices.Length; i++)
        {
            Gizmos.DrawSphere(vertices[i], 0.1f);
        }
    }

    public Vector3[] GetGridFields()
    {
        return this.gridFields;
    }
    public int GetxSize()
    {
        return this.xSize;
    }
    public int GetzSize()
    {
        return this.zSize;
    }
}