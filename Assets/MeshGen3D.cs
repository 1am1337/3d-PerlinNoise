using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class MeshGen3D : MonoBehaviour
{
    Mesh mesh;

    Vector3[] vertices;
    int[] triangles;


    public int xSize = 20;
    public int zSize = 20;
    public int ySize = 20;

    public float strength = 0.3f;

    void Start()
    {
        mesh = new Mesh();
        GetComponent<MeshFilter>().mesh = mesh;

        CreateShape();
        //UpdateMesh();
    }

    void CreateShape()
    {
        vertices = new Vector3[(xSize + 1) * (zSize + 1) * (ySize + 1)];

        for (int i = 0; i <= zSize; z++)
        {
            for (int x = 0; x <= xSize; x++)
            {
                for (int y = 0; y <= ySize; y++)
                {
                    float y = Mathf.PerlinNoise(x * strength, z * strength) * 2f;
                    float x = Mathf.PerlinNoise(y * strength, z * strength) * 2f;
                    float z = Mathf.PerlinNoise(x * strength, y * strength) * 2f;
                    vertices[i] = new Vector3(x, y, z);
                    i++;
                }
            }
        }


    }


    private void OnDrawGizmos()
    {
        if (vertices == null)
        {
            return;
        }
        for (int i = 0; i < vertices.Length; i++)
            Gizmos.DrawSphere(vertices[i], .1f);
    }
}
