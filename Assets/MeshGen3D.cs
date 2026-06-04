using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
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

    public int threshold = 128;
    public float freq = 2.0;
    public float amp = 1.0;
    public float persistence = 0.2;
    public int oct = 3;
    public int seed = 0;


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
        float z = 0;
        for (int i = 0; z <= zSize; z++)
        {
            for (float x = 0; x <= xSize; x++)
            {
                for (float y = 0; y <= ySize; y++)
                {
                    vertices[i] = new Vector3(x, y, z);
                    verticesState[i] = PerlinGen.perlin.PerlinGen3D(x, y, z, freq, amp, persistence, oct, seed);
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
            if (verticesState[i] > threshold)
            {

                byte vertIntensity = (byte)verticesState[i];
                Gizmos.color = new UnityEngine.Color32(255, 0, 0, 255);
                Gizmos.DrawSphere(vertices[i], .1f);
            }
        }
    }
}
