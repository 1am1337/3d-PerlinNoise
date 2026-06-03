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

    public byte vertIntensity = 0;



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
        for (int i = 0, z = 0; z <= zSize; z++)
        {
            for (int x = 0; x <= xSize; x++)
            {
                for (int y = 0; y <= ySize; y++)
                {
                    vertices[i] = new Vector3(x, y, z);
                    verticesState[i] = (Mathf.PerlinNoise(x, y) * 2f) * 256;
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
