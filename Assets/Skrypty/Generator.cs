using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshFilter))]
public class Generator : MonoBehaviour
{
    Mesh mesh;
    Tree tree;

    Vector3[] wierzcholki;
    int[] trojkaty;

    public int xSize = 1000;
    public int zSize = 1000;

    System.Random rand = new System.Random();
   

    // Start is called before the first frame update
    void Start()
    {
        mesh = new Mesh();
        GetComponent<MeshFilter>().mesh = mesh;

        CreateShape();
        UpdateMesh();
    }

    void CreateShape()
    {
        wierzcholki = new Vector3[(xSize + 1) * (zSize + 1)];

        int i = 0;
        for(int z = 0; z <= zSize; z++)
        {
            for (int x = 0; x <= xSize; x++)
            {
                float y = Mathf.PerlinNoise(x * .05f, z * .05f) * 05f;
                wierzcholki[i] = new Vector3(x, y, z);
                i++;
            }
        }

        trojkaty = new int[xSize * zSize * 6];
        int wierz = 0;
        int troj = 0;
        for (int z = 0; z < zSize; z++) {
            for (int x = 0; x < xSize; x++)
            {
                trojkaty[troj + 0] = wierz + 0;
                trojkaty[troj + 1] = wierz + xSize + 1;
                trojkaty[troj + 2] = wierz + 1;
                trojkaty[troj + 3] = wierz + 1;
                trojkaty[troj + 4] = wierz + xSize + 1;
                trojkaty[troj + 5] = wierz + xSize + 2;

                wierz++;
                troj += 6;
            }
            wierz++;

        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
    void UpdateMesh()
    {
        mesh.Clear();
        mesh.vertices = wierzcholki;
        mesh.triangles = trojkaty;

        mesh.RecalculateNormals();
    }

   
}
