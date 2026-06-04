using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class MeshGen3D : MonoBehaviour
{
    Mesh mesh;

    Vector3[] vertices;
    float[] verticesState;
    int[] triangles;


    public int xSize = 10;
    public int zSize = 10;
    public int ySize = 10;

    //public int threshold = 128;
    // public float freq = 2.0f;
    // public float amp = 1.0f;
    // public float persistence = 0.2f;
    // public int oct = 3;
    // public int seed = 0;


    //byte vertIntensity = 0;



    // public float strength = 0.3f;

    void Start()
    {
        mesh = new Mesh();
        GetComponent<MeshFilter>().mesh = mesh;
        CreateShape();
        UpdateMesh();

    }

    void CreateShape()
    {
        vertices = new Vector3[(xSize + 1) * (ySize + 1) * (zSize + 1)];
        verticesState = new float[vertices.Length];
        int i = 0;
        for (float z = 0; z <= zSize; z++)
        {
            for (float x = 0; x <= xSize; x++)
            {
                for (float y = 0; y <= ySize; y++)
                {
                    vertices[i] = new Vector3(x, y, z);
                    verticesState[i] = PerlinGen.Perlin.PerlinGen3D(x, y, z) * 256;
                    i++;
                }
            }

        }


    }

    void UpdateMesh()
    {
        mesh.Clear();

        mesh.vertices = vertices;
        mesh.triangles = triangles;

        mesh.RecalculateNormals();
    }


    private void OnDrawGizmos()
    {

        if (vertices == null)
        {
            return;

        }
        for (int i = 0; i < verticesState.Length; i++)
        {
            // byte vertIntensity = ;
            Gizmos.color = new UnityEngine.Color32((byte)verticesState[i], 0, 0, (byte)255);
            Gizmos.DrawSphere(vertices[i], .1f);
        }
    }
}
